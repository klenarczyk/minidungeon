using MiniDungeon.Engine.Commands;

namespace MiniDungeon.Engine.Input;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    ICommand Handle(ConsoleKey key);
}