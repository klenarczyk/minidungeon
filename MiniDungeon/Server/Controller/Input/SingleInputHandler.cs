using MiniDungeon.Server.Controller.Commands.Core;

namespace MiniDungeon.Server.Controller.Input;

public class SingleInputHandler(ConsoleKey key, ICommand command) : InputHandler
{
    public override ICommand Handle(ConsoleKey reqKey)
    {
        return reqKey == key 
            ? command 
            : base.Handle(reqKey);
    }
}