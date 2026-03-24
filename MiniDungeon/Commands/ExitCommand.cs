using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class ExitCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        context.Session.IsRunning = false;
    }
}