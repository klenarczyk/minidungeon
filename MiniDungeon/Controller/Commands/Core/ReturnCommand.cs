namespace MiniDungeon.Controller.Commands.Core;

public class ReturnCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        context.PopInputChain();
        return false;
    }
}