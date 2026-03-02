namespace MiniDungeon.Core;

public class Game
{
    private readonly GameSession _session = new();
    private readonly InputHandler _inputHandler = new();
    private readonly Renderer _renderer = new(80, 24);
    
    public void Run()
    {
        _renderer.Init();

        while (_session.IsRunning)
        {
            _renderer.Render(_session);
            
            var command = _inputHandler.GetCommand();
            command?.Execute(_session);
        }
    }
}