using MiniDungeon.Controller.Commands.Core;

namespace MiniDungeon.Controller.Input;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    ICommand Handle(ConsoleKey key);
}