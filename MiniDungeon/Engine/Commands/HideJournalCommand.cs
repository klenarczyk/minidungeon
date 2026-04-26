namespace MiniDungeon.Engine.Commands;

public class HideJournalCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        context.PopInputChain();
        context.Session.ShowJournal = false;
        return false;
    }
}