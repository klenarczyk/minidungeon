using MiniDungeon.Client.Controller.Commands;
using MiniDungeon.Client.Controller.Commands.Core;

namespace MiniDungeon.Client.Controller.Input;

public interface IInputHandler
{
    IInputHandler SetNext(IInputHandler handler);
    IClientCommand Handle(ConsoleKey key);
}