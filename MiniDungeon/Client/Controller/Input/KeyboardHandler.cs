using MiniDungeon.Client.Controller.Commands;
using MiniDungeon.Client.Controller.Commands.Core;

namespace MiniDungeon.Client.Controller.Input;

public abstract class KeyboardHandler : IInputHandler
{
    private IInputHandler? _next;
    
    public IInputHandler SetNext(IInputHandler handler)
    {
        _next = handler;
        return handler;
    }

    public virtual IClientCommand Handle(ConsoleKey key)
        => _next != null ? _next.Handle(key) : new InvalidKeyCommand();
}