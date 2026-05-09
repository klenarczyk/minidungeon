using MiniDungeon.Server.Controller.Commands.Core;

namespace MiniDungeon.Server.Controller.Input;

public interface IHandler
{
    IHandler SetNext(IHandler handler);
    ICommand Handle(ConsoleKey key);
}