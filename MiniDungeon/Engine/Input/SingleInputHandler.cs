using MiniDungeon.Engine.Commands;

namespace MiniDungeon.Engine.Input;

public class SingleInputHandler(ConsoleKey key, ICommand command) : InputHandler
{
    public override ICommand Handle(ConsoleKey reqKey)
    {
        return reqKey == key 
            ? command 
            : base.Handle(reqKey);
    }
}