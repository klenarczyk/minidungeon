using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Input;

namespace MiniDungeon.Commands;

public class InitEquipCommand(EquipmentSlot slot) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;

        session.Message = "Select item (1-9), Cancel (Bck)";

        IHandler inputChain = new SingleInputHandler(ConsoleKey.D1, new EquipCommand(slot, 0));
        var inputChainTail = inputChain;

        for (var i = 1; i < 9; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new SingleInputHandler(ConsoleKey.D1 + i, new EquipCommand(slot, i)));
        }
        
        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.Backspace, new EquipCommand(slot, -1)));
        
        context.PushInputChain(inputChain);
    }
}