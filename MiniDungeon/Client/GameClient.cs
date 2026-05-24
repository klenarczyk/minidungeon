using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MiniDungeon.Client.Controller;
using MiniDungeon.Client.Controller.Commands.Combat;
using MiniDungeon.Client.Controller.Input;
using MiniDungeon.Client.View;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;
using MiniDungeon.Shared.DTOs.State;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Client;

public class GameClient : IClientContext
{
    private TcpClient _tcpClient = null!;
    private NetworkStream _stream = null!;

    private string _ip;
    private int _port;
    
    private readonly Renderer _renderer = new(120, 24);
    private bool _isConnected;
    
    private readonly Stack<(IInputHandler Handler, string InputMode)> _inputChains = new();
    public bool ShowJournal { get; set; } = false;
    
    public string EntryMessage { get; private set; } = "";
    public string InputMode => _inputChains.Peek().InputMode;

    private bool _wasBattling = false;
    
    public GameClient(string ip, int port)
    {
        _ip = ip;
        _port = port;

        var logger = new ClientLogger();
        Journal.Initialize(logger);
    }
    
    public async Task Run()
    {
        try
        {
            Console.WriteLine($"Connecting to server ({_ip}:{_port})...");
            _tcpClient = new TcpClient(_ip, _port);
            _stream = _tcpClient.GetStream();
            _isConnected = true;

            Console.Clear();
            Console.CursorVisible = false;
            
            using var reader = new StreamReader(_stream, Encoding.UTF8, leaveOpen: true);
            var handshakeJson = await reader.ReadLineAsync();
            if (handshakeJson == null) throw new Exception("Handshake not received");

            var handshake = JsonSerializer.Deserialize<HandshakeDto>(handshakeJson) ??
                            throw new Exception("Invalid handshake");
            
            var (baseChain, instructions) = ActionTranslator.Build(handshake);
            PushInputChain(baseChain);
            
            _renderer.UpdateInstructions(instructions);
            EntryMessage = handshake.EntryMessage;
            
            _ = Task.Run(() => ServerUpdateLoop(reader));
            InputLoop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection Failed: {ex.Message}");
        }
    }

    private async Task ServerUpdateLoop(StreamReader reader)
    {
        try
        {
            while (_isConnected)
            {
                var json = await reader.ReadLineAsync();
                if (json == null) break;
                
                var gameState = JsonSerializer.Deserialize<GameStateDto>(json);
                if (gameState == null) continue;

                foreach (var log in gameState.JournalLogs)
                {
                    Journal.Instance.Log(log);
                }
                
                if (gameState.Player.IsBattling && !_wasBattling)
                {
                    var initBattleCmd = new InitBattleCommand();
                    initBattleCmd.Execute(this);
                    _wasBattling = true;
                } else if (!gameState.Player.IsBattling && _wasBattling)
                {
                    PopInputChain();
                    _wasBattling = false;
                }
                
                _renderer.Render(this, gameState);

                if (gameState.Player.Attributes.Health <= 0) break;
            }
        }
        catch (Exception)
        {
            // 
        }
        finally
        {
            Console.WriteLine("Game Over!");
            Environment.Exit(0);
        }
    }

    private void InputLoop()
    {
        try
        {
            while (_isConnected)
            {
                var key = Console.ReadKey(true).Key;

                var currentHandler = _inputChains.Peek().Handler;
                var command = currentHandler.Handle(key);
                
                var updateUi = command.Execute(this);
                if (updateUi) _renderer.Render(this);
            }
        }
        catch (IOException)
        {
            _isConnected = false;
        }
    }
    
    // ---
    
    public void PushInputChain(IInputHandler chainHead, string inputMode = "") 
        => _inputChains.Push((chainHead, inputMode));
    public void PopInputChain() => _inputChains.Pop();

    public void SendToServer(CommandEnvelope envelope)
    {
        if (!_isConnected) return;
        
        var json = JsonSerializer.Serialize(envelope);
        var bytes = Encoding.UTF8.GetBytes(json + "\n");
        _stream.Write(bytes, 0, bytes.Length);
    }
}