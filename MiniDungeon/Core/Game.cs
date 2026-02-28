namespace MiniDungeon.Core;

public class Game
{
    private readonly GameSession _session = new();
    
    public void Run()
    {
        Renderer.Init();
        Renderer.Draw(_session);
    }
}