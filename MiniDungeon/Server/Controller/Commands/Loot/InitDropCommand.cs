using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Controller.Input;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class InitDropCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        IHandler inputChain = new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        var inputChainTail = inputChain;

        for (var i = 0; i < 9; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new SingleInputHandler(ConsoleKey.D1 + i, new DropCommand(i)));
        }
        
        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain, "Drop");
        return false;
    }
}