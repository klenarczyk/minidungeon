namespace MiniDungeon.Client.Controller.Commands.Core;

public class ReturnCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        context.PopInputChain();
        return true;
    }
}