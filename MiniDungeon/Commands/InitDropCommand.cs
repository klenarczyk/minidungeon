using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Input;

namespace MiniDungeon.Commands;

public class InitDropCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;

        session.Message = "Select item (1-9), Cancel (Bck)";

        IHandler inputChain = new SingleInputHandler(ConsoleKey.D1, new DropCommand(0));
        var inputChainTail = inputChain;

        for (var i = 1; i < 9; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new SingleInputHandler(ConsoleKey.D1 + i, new DropCommand(i)));
        }
        
        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain);
    }
}