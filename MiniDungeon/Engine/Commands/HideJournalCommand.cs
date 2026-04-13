namespace MiniDungeon.Engine.Commands;

public class HideJournalCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        context.PopInputChain();
        context.Session.ShowJournal = false;
    }
}