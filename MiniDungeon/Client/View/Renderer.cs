using MiniDungeon.Client.View.Displays;
using MiniDungeon.Server.Logging;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Client.View;

public class Renderer
{
    private readonly RenderBuffer _buffer;
    private readonly List<IDisplayElement> _components = [];

    public Renderer(int width, int height)
    {
        _buffer = new RenderBuffer(width, height);
        
        _components.Add(new BoardDisplay());
        _components.Add(new StatDisplay());
        _components.Add(new SidebarDisplay());
        _components.Add(new MessageDisplay());
    }
    
    public void Render(GameStateDto gameState)
    {
        if (gameState.ShowJournal) RenderLogs();
        else RenderGame(gameState);
    }

    private void RenderGame(GameStateDto gameState)
    {
        _buffer.Clear();

        foreach (var element in _components)
        {
            element.Draw(_buffer, gameState);
        }
        
        _buffer.Draw();
    }

    // TODO: Add exclusive journal logs
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
} 