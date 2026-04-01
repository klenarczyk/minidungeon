using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Input;

namespace MiniDungeon.Commands;

public class InitEquipCommand(EquipmentSlot slot) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;

        IHandler inputChain = new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        var inputChainTail = inputChain;

        for (var i = 0; i < 9; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new SingleInputHandler(ConsoleKey.D1 + i, new EquipCommand(slot, i)));
        }
        
        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain, "Equip");
        
        session.Message = "Select item (1-9), Cancel (Bck)";
    }
}