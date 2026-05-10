using System.Net.Sockets;
using System.Text.Json;
using MiniDungeon.Server.Controller.Input;
using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Server.Network;

public class ClientHandler : IGameContext
{
    private readonly TcpClient _tcpClient;
    private readonly GameServer _server;
    private StreamWriter _writer = null!;

    private int PlayerId { get; }
    public Player Player => _server.Session.Players[PlayerId];
    public GameSession Session => _server.Session;
    
    private readonly Stack<(IHandler Handler, string InputMode)> _inputChains = new();
    public string CurrentInputMode => _inputChains.Peek().InputMode;
    
    public bool ShowJournal { get; set; } = false;

    public ClientHandler(TcpClient tcpClient, GameServer server, int playerId)
    {
        _tcpClient = tcpClient;
        _server = server;
        PlayerId = playerId;
        
        PushInputChain(_server.BaseInputChain);
    }
    
    public void PushInputChain(IHandler chainHead, string inputMode = "") 
        => _inputChains.Push((chainHead, inputMode));
    
    public void PopInputChain() => _inputChains.Pop();
    
    // ---
    
    public async Task HandleClientLoop()
    {
        await using var stream = _tcpClient.GetStream();
        using var reader = new StreamReader(stream);

        await using var writer = new StreamWriter(stream) { AutoFlush = true };
        _writer = writer;
        
        try
        {
            while (_tcpClient.Connected)
            {
                var json = await reader.ReadLineAsync();
                if (json == null) break;

                var keyDto = JsonSerializer.Deserialize<KeyPressDto>(json);
                if (keyDto == null) continue;
                
                var currentHandler = _inputChains.Peek().Handler;
                var command = currentHandler.Handle(keyDto.Key);
                
                lock (_server.Lock)
                {
                    var timePassed = command.Execute(this);

                    if (timePassed)
                    {
                        foreach (var entity in Session.Entities)
                        {
                            if (entity.BattledPlayerId != null) continue;
                            entity.Move(this);
                        }
                    }
                }

                await _server.BroadcastStateAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Client {PlayerId} disconnected");
            _tcpClient.Close();

            lock (_server.Lock)
            {
                foreach (var entity in Session.Entities)
                {
                    if (entity.BattledPlayerId != PlayerId) continue;
                    entity.BattledPlayerId = null;
                }
                
                _server.Clients.Remove(this);
                _server.Session.Players.Remove(PlayerId);
            }
            
            await _server.BroadcastStateAsync();
        }
    }

    public async Task SendMessageAsync(string json)
    {
        if (_tcpClient.Connected && _writer != null)
        {
            await _writer.WriteLineAsync(json);
        }
    }
}