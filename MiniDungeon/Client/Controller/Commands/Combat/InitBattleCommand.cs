using MiniDungeon.Client.Controller.Commands.Core;
using MiniDungeon.Client.Controller.Commands.Loot;
using MiniDungeon.Client.Controller.Input;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Combat;

public class InitBattleCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        IInputHandler inputChain = new InputHandler(ConsoleKey.Escape, new RequestExitCommand());
        var inputChainTail = inputChain;

        inputChainTail = inputChainTail.SetNext(
            new InputHandler(ConsoleKey.D1, 
                new RequestAttackCommand(AttackType.Normal)));
        
        inputChainTail = inputChainTail.SetNext(
            new InputHandler(ConsoleKey.D2, 
                new RequestAttackCommand(AttackType.Stealth)));
        
        inputChainTail = inputChainTail.SetNext(
            new InputHandler(ConsoleKey.D3, 
                new RequestAttackCommand(AttackType.Magic)));

        inputChainTail = inputChainTail.SetNext(
            new InputHandler(ConsoleKey.L, new InitEquipCommand(EquipmentSlot.LeftHand)));
        
        inputChainTail = inputChainTail.SetNext(
            new InputHandler(ConsoleKey.R, new InitEquipCommand(EquipmentSlot.RightHand)));
            
        inputChainTail.SetNext(
            new InputHandler(ConsoleKey.Backspace, new RequestFleeCommand()));
        
        context.PushInputChain(inputChain, "Battle");
        return true;
    }
}