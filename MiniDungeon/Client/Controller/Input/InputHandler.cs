using MiniDungeon.Client.Controller.Commands;
using MiniDungeon.Client.Controller.Commands.Core;

namespace MiniDungeon.Client.Controller.Input;

public class InputHandler(ConsoleKey key, IClientCommand command) : KeyboardHandler
{
    public override IClientCommand Handle(ConsoleKey reqKey) 
        => reqKey == key ? command : base.Handle(reqKey);
}