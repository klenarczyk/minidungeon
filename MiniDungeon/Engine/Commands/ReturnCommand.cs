namespace MiniDungeon.Engine.Commands;

public class ReturnCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        context.PopInputChain();
    }
}