using MiniDungeon.Engine.Commands;

namespace MiniDungeon.Engine.Input;

public abstract class InputHandler : IHandler
{
    private IHandler? _nextHandler;
    
    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual ICommand Handle(ConsoleKey key)
    {
        return _nextHandler != null 
            ? _nextHandler.Handle(key) 
            : new InvalidKeyCommand();
    }
}