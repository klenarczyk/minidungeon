using MiniDungeon.Commands;
using MiniDungeon.Components;

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
        { ConsoleKey.L, new EquipCommand(EquipmentSlot.LeftHand) },
        { ConsoleKey.R, new EquipCommand(EquipmentSlot.RightHand) },
        { ConsoleKey.D1, new SelectItemCommand(0)},
        { ConsoleKey.D2, new SelectItemCommand(1)},
        { ConsoleKey.D3, new SelectItemCommand(2)},
        { ConsoleKey.D4, new SelectItemCommand(3)},
        { ConsoleKey.D5, new SelectItemCommand(4)},
        { ConsoleKey.D6, new SelectItemCommand(5)},
        { ConsoleKey.D7, new SelectItemCommand(6)},
        { ConsoleKey.D8, new SelectItemCommand(7)},
        { ConsoleKey.D9, new SelectItemCommand(8)},
        { ConsoleKey.Escape, new ExitCommand() }
    };
    
    public ICommand? GetCommand()
    {
        var keyInfo = Console.ReadKey(true);

        return _keyBindings.GetValueOrDefault(keyInfo.Key);
    }
}