using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Controller.Input;
using MiniDungeon.Server.Model.Loot;

namespace MiniDungeon.Server.Controller.Commands.Loot;

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