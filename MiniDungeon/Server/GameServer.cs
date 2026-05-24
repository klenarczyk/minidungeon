using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using MiniDungeon.Server.Controller;
using MiniDungeon.Server.Controller.Actions;
using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.World.Generation;
using MiniDungeon.Server.Model.World.Themes;
using MiniDungeon.Shared.Configuration;
using MiniDungeon.Shared.DTOs.State;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server;

public class GameServer
{
    private readonly int _port;
    private readonly TcpListener _listener;
    
    public readonly Lock Lock = new();
    
    public GameSession Session { get; }
    public IEnvelopeHandler CommandChain { get; }

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
        var actionBuilder = new ActionBuilder();
        
        theme.GenerationTemplate.Use(layoutBuilder);
        theme.GenerationTemplate.Use(actionBuilder);
        
        CommandChain = actionBuilder.GetCommandChain() ?? new ExitParser();
        
        var board = layoutBuilder.Build();
        
        Session = new GameSession(board)
        {
            ActionList = actionBuilder.ActionList,
            EntryMessage = theme.EntryMessage,
            Entities = entityList,
        };
    }

    public async Task Run()
    {
        _listener.Start();
        Console.WriteLine($"Server is running on port {_port}...");

        try
        {
            while (true)
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
                    
                    var clientHandler = new ClientHandler(newClient, this, newPlayer);
                    Clients.Add(clientHandler);
                    _ = Task.Run(clientHandler.HandleClientLoop);
                    success = true;
                }

                if (success) await BroadcastStateAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}.");
        }
        finally
        {
            Journal.Instance.OnExit();
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
        List<(ClientHandler client, string json)> payloads = [];

        lock (Lock)
        {
            foreach (var client in Clients)
            {
                if (!client.IsRunning) continue;
            
                var dto = GenerateSateDto(client); 
                var json = JsonSerializer.Serialize(dto);
                payloads.Add((client, json));
            }
        }

        foreach (var payload in payloads)
        {
            tasks.Add(payload.client.SendMessageAsync(payload.json));
        }
    
        await Task.WhenAll(tasks);
    }

    private GameStateDto GenerateSateDto(ClientHandler client)
    {
        var logs = client.PlayerLogs.ToList();
        client.PlayerLogs.Clear();
        
        return new GameStateDto(
            client.Player.ToDto(),
            logs,
            Session.Board.ToWallsDto(),
            Session.Board.ToItemsDto(),
            Session.Entities.ToDto(),
            Session.Players.ToDto()
            );
    }
}