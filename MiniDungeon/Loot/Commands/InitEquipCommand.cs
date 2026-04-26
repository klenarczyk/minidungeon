using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Input;

namespace MiniDungeon.Loot.Commands;

public class InitEquipCommand(EquipmentSlot slot) : ICommand
{
    public bool Execute(IGameContext context)
    {
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
        return false;
    }
}