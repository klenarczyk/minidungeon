using MiniDungeon.Commands;
using MiniDungeon.Components;
using MiniDungeon.World.Generation;

namespace MiniDungeon.Input;

public class InstructionBuilder : IDungeonBuilder
{
    private readonly List<string> _instructions = [];
    private readonly HashSet<ConsoleKey> _keyBinds = [];

    private IHandler? _inputChainHead;
    private IHandler? _inputChainTail;

    public InstructionBuilder() => Reset();
    
    public IDungeonBuilder InitializeEmpty()
    {
        Reset();
        return this;
    }

    public IDungeonBuilder InitializeFilled()
    {
        Reset();
        return this;
    }

    public IDungeonBuilder AddCorridors() => this;

    public IDungeonBuilder AddRooms() => this;

    public IDungeonBuilder AddCentralRoom(int width, int height) => this;

    public IDungeonBuilder AddItems(int count)
    {
        BindItemCommands();        
        return this;
    }

    public IDungeonBuilder AddWeapons()
    {
        BindItemCommands();
        
        // Attack actions will be added later I assume
        
        return this;
    }

    public IHandler? GetInputChain() => _inputChainHead;

    public string GetInstructions() => string.Join(", ",  _instructions);
    
    // Helpers
    private void BindItemCommands()
    {
        BindKey(ConsoleKey.D1, new SelectItemCommand(0), "Inventory (1-9)");
        for (var i = 1; i < 9; i++)
        {
            BindKey(ConsoleKey.D1 + i, new SelectItemCommand(i));
        }
        
        BindKey(ConsoleKey.E, new PickUpCommand(), "Pick up (E)");
        BindKey(ConsoleKey.Q, new InitDropCommand(), "Drop (Q)");
        
        BindKey(ConsoleKey.L, new InitEquipCommand(EquipmentSlot.LeftHand), "Equip/Unequip (L/R)");
        BindKey(ConsoleKey.R, new InitEquipCommand(EquipmentSlot.RightHand));
    }
    
    private void BindKey(ConsoleKey key, ICommand command, string? instruction = null)
    {
        if (!_keyBinds.Add(key)) return; // Already bound

        var handler = new SingleInputHandler(key, command);

        if (_inputChainHead == null ||  _inputChainTail == null)
        {
            _inputChainHead = handler;
            _inputChainTail = handler;
        }
        else
        {
            _inputChainTail = _inputChainTail.SetNext(handler);
        }

        if (instruction == null || instruction.IsWhiteSpace()) return;
        _instructions.Add(instruction);
    }

    private void Reset()
    {
        _keyBinds.Clear();
        _instructions.Clear();
        
        BindKey(ConsoleKey.Escape, new ExitCommand(), "Quit (Esc)");
        
        BindKey(ConsoleKey.W, new MoveCommand(0, -1), "Move (WASD)");
        BindKey(ConsoleKey.A, new MoveCommand(-1, 0));
        BindKey(ConsoleKey.S, new MoveCommand(0, 1));
        BindKey(ConsoleKey.D, new MoveCommand(1, 0));
    }
}