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
        _buffer.Clear();

        foreach (var element in _components)
        {
            element.Draw(_buffer, session);
        }
        
        _buffer.Draw();
    }

    public void Init()
    {
        Console.Clear();
        Console.CursorVisible = false;
    }
    
    public void UpdateInstructions(string instructions) => _buffer.Instructions = instructions;
} 