namespace MiniDungeon.Core;

public class Game
{
    private readonly GameSession _session = new();
    private readonly InputHandler _inputHandler = new();
    
    public void Run()
    {
        Renderer.Init();
        Renderer.Draw(_session);

        while (_session.IsRunning)
        {
            var command = _inputHandler.GetCommand();
            if (command == null) continue;
            
            command.Execute(_session);
            Renderer.Draw(_session);
        }
    }
}