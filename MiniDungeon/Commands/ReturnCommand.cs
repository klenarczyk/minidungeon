using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class ReturnCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        context.Session.Message = "";
        context.PopInputChain();
    }
}