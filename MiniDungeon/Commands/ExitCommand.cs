using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class ExitCommand : ICommand
{
    public void Execute(GameSession session)
    {
        session.IsRunning = false;
    }
}