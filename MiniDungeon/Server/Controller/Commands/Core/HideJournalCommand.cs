namespace MiniDungeon.Server.Controller.Commands.Core;

public class HideJournalCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        context.PopInputChain();
        context.ShowJournal = false;
        return false;
    }
}