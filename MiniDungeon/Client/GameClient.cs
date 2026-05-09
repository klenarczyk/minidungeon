using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MiniDungeon.Client.View;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Client;

public class GameClient(string ip, int port)
{
    private TcpClient _tcpClient = null!;
    private NetworkStream _stream = null!;

    private readonly Renderer _renderer = new(120, 24);
    private bool _isConnected;

    public void Run()
    {
        try
        {
            Console.WriteLine($"Connecting to server ({ip}:{port})...");
            _tcpClient = new TcpClient(ip, port);
            _stream = _tcpClient.GetStream();
            _isConnected = true;

            Console.Clear();
            Console.CursorVisible = false;
            
            Task.Run(() => ServerUpdateLoop());
            InputLoop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to server: {ex.Message}");
        }
    }

    private async Task ServerUpdateLoop()
    {
        using var reader = new StreamReader(_stream, Encoding.UTF8);

        try
        {
            while (_isConnected)
            {
                var json = await reader.ReadLineAsync();
                if (json == null) break;

                var gameState = JsonSerializer.Deserialize<GameStateDto>(json);
                if (gameState != null)
                {
                    _renderer.Render(gameState);
                }
            }
        }
        catch (Exception)
        {
            // 
        }
        finally
        {
            Console.WriteLine("Connection closed");
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

                var dto = new KeyPressDto(key);
                var json = JsonSerializer.Serialize(dto);

                var bytes = Encoding.UTF8.GetBytes(json + "\n");
                _stream.Write(bytes, 0, bytes.Length);
            }
        }
        catch (IOException)
        {
            _isConnected = false;
        }
    }
}