namespace MiniDungeon.Server.Controller.Commands.Core;

public class ExitCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        context.Session.IsRunning = false;
        return false;
    }
}