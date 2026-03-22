using MiniDungeon.Commands;

namespace MiniDungeon.Input;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    ICommand Handle(ConsoleKey key);
}