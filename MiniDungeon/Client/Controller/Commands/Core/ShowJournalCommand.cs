using MiniDungeon.Client.Controller.Input;

namespace MiniDungeon.Client.Controller.Commands.Core;

public class ShowJournalCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var inputChain = new InputHandler(ConsoleKey.Escape, new RequestExitCommand());
        var inputChainTail = inputChain;

        inputChainTail.SetNext(
            new InputHandler(ConsoleKey.J, new HideJournalCommand()));
        
        context.PushInputChain(inputChain, "Journal");
        context.ShowJournal = true;
        return true;
    }
}