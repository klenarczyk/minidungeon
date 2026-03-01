using MiniDungeon.Commands;

namespace MiniDungeon.Core;

public class InputHandler
{
    private readonly Dictionary<ConsoleKey, ICommand> _keyBindings = new()
    {
        { ConsoleKey.W, new MoveCommand(0, -1) },
        { ConsoleKey.A, new MoveCommand(-1, 0) },
        { ConsoleKey.S, new MoveCommand(0, 1) },
        { ConsoleKey.D, new MoveCommand(1, 0) },
        { ConsoleKey.E, new PickUpCommand() },
        { ConsoleKey.Q, new DropCommand() },
        { ConsoleKey.Escape, new ExitCommand() }
    };
    
    public ICommand? GetCommand()
    {
        var keyInfo = Console.ReadKey(true);

        return _keyBindings.GetValueOrDefault(keyInfo.Key);
    }
}