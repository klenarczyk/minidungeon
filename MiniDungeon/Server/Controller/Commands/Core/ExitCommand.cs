namespace MiniDungeon.Server.Controller.Commands.Core;

public class ExitCommand : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        context.IsRunning = false;
        return false;
    }
}