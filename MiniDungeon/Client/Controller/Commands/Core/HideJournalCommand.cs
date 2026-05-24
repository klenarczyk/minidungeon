namespace MiniDungeon.Client.Controller.Commands.Core;

public class HideJournalCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        context.PopInputChain();
        context.ShowJournal = false;
        return true;
    }
}