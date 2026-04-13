using MiniDungeon.Combat.Attacks;
using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Input;
using MiniDungeon.Loot;
using MiniDungeon.Loot.Commands;
using MiniDungeon.World;

namespace MiniDungeon.Combat;

public class InitBattleCommand(Cell cell) : ICommand
{
    public void Execute(IGameContext context)
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
    }
}