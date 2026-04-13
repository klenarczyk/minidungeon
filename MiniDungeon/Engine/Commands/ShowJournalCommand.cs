using MiniDungeon.Engine.Input;

namespace MiniDungeon.Engine.Commands;

public class ShowJournalCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        IHandler inputChain = new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        var inputChainTail = inputChain;

        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.J, new HideJournalCommand()));
        
        context.PushInputChain(inputChain, "Journal");
        context.Session.ShowJournal = true;
    }
}