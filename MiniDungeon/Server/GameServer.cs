using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Controller.Input;
using MiniDungeon.Server.Logging;
using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.World.Generation;
using MiniDungeon.Server.Model.World.Themes;
using MiniDungeon.Server.Network;
using MiniDungeon.Shared.Configuration;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Server;

public class GameServer
{
    private readonly int _port;
    private readonly TcpListener _listener;
    
    public readonly Lock Lock = new();
    
    public GameSession Session { get; }
    public IHandler BaseInputChain { get; }

    public List<ClientHandler> Clients { get; } = [];
    private const int ServerCapacity = 9;
    private readonly Config _config;
    
    public GameServer(int port)
    {
        _port = port;
        _listener = new TcpListener(IPAddress.Any, _port);
        
        // ---
        
        var configPath = Path.Combine(AppContext.BaseDirectory, "Shared", "Configuration", "config.json");
        _config = Config.Load(configPath);
        
        var logger = new FileLogger(_config);
        Journal.Initialize(logger);
        
        var theme = new WorkshopTheme();

        var entityList = new List<IEntity>();
        var layoutBuilder = new LayoutBuilder(theme, entityList);
        var instructionBuilder = new InstructionBuilder();
        
        theme.GenerationTemplate.Use(layoutBuilder);
        theme.GenerationTemplate.Use(instructionBuilder);

        var baseInputChain = instructionBuilder.GetInputChain();
        BaseInputChain = baseInputChain ?? new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        
        var board = layoutBuilder.Build();
        
        Session = new GameSession(board)
        {
            EntryMessage = theme.EntryMessage,
            Instructions = instructionBuilder.GetInstructions(),
            
            Entities = entityList,
        };
    }

    public async Task Run()
    {
        _listener.Start();
        Session.IsRunning = true;
        Console.WriteLine($"Server is running on port {_port}...");

        while (Session.IsRunning)
        {
            try
            {
                var newClient = await _listener.AcceptTcpClientAsync();
                Console.WriteLine($"New client connected: {newClient.Client.RemoteEndPoint}.");

                bool success;
                lock (Lock)
                {
                    if (Session.Players.Count >= ServerCapacity)
                    {
                        Console.WriteLine("Server is full.");
                        newClient.Close();
                        continue;
                    }

                    var id = GetAvailableId();
                    
                    var newPlayer = new Player(Session.Board.StartingPosition)
                    {
                        Id = id,
                        Name = _config.PlayerName
                    };

                    Session.Players.Add(id, newPlayer);
                    Console.WriteLine($"Player {id} ({newClient.Client.RemoteEndPoint}) connected.");
                    Journal.Instance.Log($"Player {id} ({newClient.Client.RemoteEndPoint}) connected.");
                    
                    var clientHandler = new ClientHandler(newClient, this, id);
                    Clients.Add(clientHandler);
                    _ = Task.Run(clientHandler.HandleClientLoop);
                    success = true;
                }

                if (success) await BroadcastStateAsync();
            }
            catch (Exception ex)
            {
                if (Session.IsRunning)
                {
                    Console.WriteLine($"Error while accepting connection: {ex.Message}.");
                }
            }
            finally
            {
                Journal.Instance.OnExit();
            }
            
        }
    }
    
    private int GetAvailableId()
    {
        for (var i = 1; i <= 9; i++)
        {
            if (Session.Players.ContainsKey(i)) continue;
            return i;
        }
        
        throw new InvalidOperationException("No available player IDs!"); 
    }

    public async Task BroadcastStateAsync()
    {
        List<Task> tasks = [];

        List<ClientHandler> snapshot;
        lock (Lock)
        {
            snapshot = Clients.ToList();
        }

        foreach (var client in snapshot)
        {
            var dto = GenerateSateDto(client);
            var json = JsonSerializer.Serialize(dto);
            tasks.Add(client.SendMessageAsync(json));
        }
        
        await Task.WhenAll(tasks);
    }
    
    private GameStateDto GenerateSateDto(ClientHandler client) => new(
        client.Player.ToDto(),
        client.CurrentInputMode,
        client.PlayerLogs.Count == 0 ? Session.EntryMessage : client.PlayerLogs[^1],
        Session.Instructions,
        client.PlayerLogs,
        client.ShowJournal,
        Session.Board.ToWallsDto(),
        Session.Board.ToItemsDto(),
        Session.Entities.ToDto(),
        Session.Players.ToDto());
}