using System.Net.Sockets;
using System.Text.Json;
using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server;

public class ClientHandler(TcpClient tcpClient, GameServer server, Player player) : IServerContext
{
    private StreamWriter? _writer;
    public bool IsRunning { get; set; }

    public Player Player { get; } = player;
    private int PlayerId => Player.Id;
    public GameSession Session => server.Session;
    
    public List<string> PlayerLogs { get; } = [];

    // ---
    
    public async Task HandleClientLoop()
    {
        try
        {
            await using var stream = tcpClient.GetStream();
            using var reader = new StreamReader(stream);

            await using var writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            _writer = writer;
            
            var handshake = new HandshakeDto()
            {
                AllowedActions = Session.ActionList,
                EntryMessage = Session.EntryMessage
            };
            
            var handshakeJson = JsonSerializer.Serialize(handshake);
            await SendMessageAsync(handshakeJson);
            IsRunning = true;
            
            await server.BroadcastStateAsync();
            
            while (IsRunning && !Player.IsDead)
            {
                var json = await reader.ReadLineAsync();
                if (json == null) break;

                var commandEnvelope = JsonSerializer.Deserialize<CommandEnvelope>(json);
                if (commandEnvelope == null) continue;
                
                var command = server.CommandChain.Handle(commandEnvelope);

                lock (server.Lock)
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

                await server.BroadcastStateAsync();
            }
        }
        catch (IOException)
        {
            Console.WriteLine($"Client {PlayerId} connection dropped.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error (client handler {PlayerId}): {ex.Message}");
        }
        finally
        {
            _writer = null;
            
            Console.WriteLine($"Client {PlayerId} disconnected.");
            Journal.Instance.Log($"Player {PlayerId} disconnected.");
            
            tcpClient.Close();

            lock (server.Lock)
            {
                foreach (var entity in Session.Entities)
                {
                    if (entity.BattledPlayerId != PlayerId) continue;
                    entity.BattledPlayerId = null;
                }
                
                server.Clients.Remove(this);
                server.Session.Players.Remove(PlayerId);
            }
            
            await server.BroadcastStateAsync();
        }
    }

    public async Task SendMessageAsync(string json)
    {
        var writer = _writer;
        if (writer == null) return;
        
        try
        {
            await writer.WriteLineAsync(json);
        }
        catch (Exception ex) when (ex is IOException or ObjectDisposedException or SocketException)
        {
            _writer = null; 
        }
    }
}