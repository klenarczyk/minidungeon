using MiniDungeon.Combat;
using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Input;
using MiniDungeon.World;

namespace MiniDungeon.Commands;

public class InitBattleCommand(Cell cell) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        
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
        
        session.Message = "Attack (1-3), Equip (L/R), Cancel (Bck)";
    }
}