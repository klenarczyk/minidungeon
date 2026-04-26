namespace MiniDungeon.Engine.Commands;

public class ReturnCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        context.PopInputChain();
        return false;
    }
}