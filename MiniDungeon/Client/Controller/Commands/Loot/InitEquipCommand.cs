using MiniDungeon.Client.Controller.Commands.Core;
using MiniDungeon.Client.Controller.Input;
using MiniDungeon.Server.Model.Loot;

namespace MiniDungeon.Client.Controller.Commands.Loot;

public class InitEquipCommand(EquipmentSlot slot) : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        IInputHandler inputChain = new InputHandler(ConsoleKey.Escape, new RequestExitCommand());
        var inputChainTail = inputChain;

        for (var i = 0; i < 9; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new InputHandler(ConsoleKey.D1 + i, new RequestEquipCommand(slot, i)));
        }
        
        inputChainTail.SetNext(
            new InputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain, "Equip");
        return true;
    }
}