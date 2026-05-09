using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Controller.Commands.Loot;
using MiniDungeon.Server.Controller.Input;
using MiniDungeon.Server.Model.Combat.Attacks;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Controller.Commands.Combat;

public class InitBattleCommand(Cell cell) : ICommand
{
    public bool Execute(IGameContext context)
    {
        IHandler inputChain = new SingleInputHandler(ConsoleKey.Escape, new ExitCommand());
        var inputChainTail = inputChain;

        inputChainTail = inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.D1, 
                new AttackCommand(cell, new NormalAttackVisitor())));
        
        inputChainTail = inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.D2, 
                new AttackCommand(cell, new StealthAttackVisitor())));
        
        inputChainTail = inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.D3, 
                new AttackCommand(cell, new MagicAttackVisitor())));

        inputChainTail = inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.L, new InitEquipCommand(EquipmentSlot.LeftHand)));
        
        inputChainTail = inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.R, new InitEquipCommand(EquipmentSlot.RightHand)));
            
        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain, "Battle");
        return false;
    }
}