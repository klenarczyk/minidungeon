using MiniDungeon.Core;
using MiniDungeon.Input;
using MiniDungeon.World;

namespace MiniDungeon.Commands;

public class InitAttackCommand(Cell cell) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;

        session.Message = "Attack (1-3), Equip (L/R), Cancel (Bck)";

        IHandler inputChain = new SingleInputHandler(ConsoleKey.D1, new AttackCommand(cell, 0));
        var inputChainTail = inputChain;

        for (var i = 1; i < 3; i++)
        {
            inputChainTail = inputChainTail.SetNext(
                new SingleInputHandler(ConsoleKey.D1 + i, new AttackCommand(cell, i)));
        }
        
        inputChainTail.SetNext(
            new SingleInputHandler(ConsoleKey.Backspace, new ReturnCommand()));
        
        context.PushInputChain(inputChain);
    }
}