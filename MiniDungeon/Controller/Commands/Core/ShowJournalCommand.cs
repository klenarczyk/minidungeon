using MiniDungeon.Controller.Input;

namespace MiniDungeon.Controller.Commands.Core;

public class ShowJournalCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        IHandler inputChain = new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        var inputChainTail = inputChain;

        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.J, new HideJournalCommand()));
        
        context.PushInputChain(inputChain, "Journal");
        context.Session.ShowJournal = true;
        return false;
    }
}