namespace MiniDungeon.Engine.Commands;

public class ExitCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        context.Session.IsRunning = false;
    }
}