using MiniDungeon.Engine.Logging;
using MiniDungeon.Engine.UI.Displays;

namespace MiniDungeon.Engine.UI;

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
    
    public void Render(GameSession session)
    {
        if (session.ShowJournal) RenderLogs();
        else RenderGame(session);
    }

    public void Init()
    {
        Console.Clear();
        Console.CursorVisible = false;
    }
    
    public void UpdateInstructions(string instructions) => _buffer.Instructions = instructions;

    private void RenderGame(GameSession session)
    {
        _buffer.Clear();

        foreach (var element in _components)
        {
            element.Draw(_buffer, session);
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
} 