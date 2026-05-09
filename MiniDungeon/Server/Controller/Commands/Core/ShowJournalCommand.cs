using MiniDungeon.Server.Controller.Input;

namespace MiniDungeon.Server.Controller.Commands.Core;

public class ShowJournalCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        IHandler inputChain = new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        var inputChainTail = inputChain;

        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.J, new HideJournalCommand()));
        
        context.PushInputChain(inputChain, "Journal");
        context.ShowJournal = true;
        return false;
    }
}