using MiniDungeon.Client.Controller.Commands.Core;
using MiniDungeon.Client.Controller.Input;

namespace MiniDungeon.Client.Controller.Commands.Loot;

public class InitDropCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        IInputHandler inputChain = new InputHandler(ConsoleKey.Escape, new RequestExitCommand());
        var inputChainTail = inputChain;

        for (var i = 0; i < 9; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new InputHandler(ConsoleKey.D1 + i, new RequestDropCommand(i)));
        }
        
        inputChainTail.SetNext(
            new InputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain, "Drop");
        return true;
    }
}