using MiniDungeon.Client.View.Displays;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.State;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Client.View;

public class Renderer
{
    private readonly RenderBuffer _buffer;
    private readonly List<IDisplayElement> _components = [];

    private Lock _renderLock = new();
    private GameStateDto? _loadedGameState;

    public Renderer(int width, int height)
    {
        _buffer = new RenderBuffer(width, height);
        
        _components.Add(new BoardDisplay());
        _components.Add(new StatDisplay());
        _components.Add(new SidebarDisplay());
        _components.Add(new MessageDisplay());
    }
    
    public void Render(IClientContext context, GameStateDto? gameState = null)
    {
        lock (_renderLock)
        {
            if (gameState != null) _loadedGameState = gameState;
            if (_loadedGameState == null) return;
        
            if (context.ShowJournal) RenderLogs();
            else RenderGame(context, _loadedGameState);
        }
    }

    private void RenderGame(IClientContext context, GameStateDto gameState)
    {
        _buffer.Clear();

        foreach (var element in _components)
        {
            element.Draw(_buffer, context, gameState);
        }
        
        _buffer.Draw();
    }

    private void RenderLogs()
    {
        _buffer.Clear();
        var currentY = 0;
    
        DrawLine("Journal Entries:");
    
        var entries = Journal.Instance.Entries;
        var rows = _buffer.Height - 1;
        var startIdx = Math.Max(0, entries.Count - rows);
        
        for (var i = startIdx; i < entries.Count; i++) 
            DrawLine(">> " + entries[i]);
        
        _buffer.Draw();
        return;
        
        void DrawLine(string text)
            => _buffer.SetString(0, currentY++, text.PadRight(_buffer.Width));
    }

    public void UpdateInstructions(string instructions) => _buffer.Instructions = instructions;
} 