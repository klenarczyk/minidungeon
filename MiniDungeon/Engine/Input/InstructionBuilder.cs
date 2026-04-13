using MiniDungeon.Engine.Commands;
using MiniDungeon.Loot;
using MiniDungeon.Loot.Commands;
using MiniDungeon.World;
using MiniDungeon.World.Generation;

namespace MiniDungeon.Engine.Input;

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

    public IDungeonBuilder AddItems(int count) => this;

    public IDungeonBuilder AddWeapons() => this;

    public IDungeonBuilder AddEnemies()
    {
        AddInstruction(ConsoleKey.D1, "Normal attack (1)", false);
        AddInstruction(ConsoleKey.D2, "Stealth attack (2)", false);
        AddInstruction(ConsoleKey.D3, "Magic attack (3)", false);
        
        return this;
    }
    
    public IHandler? GetInputChain() => _inputChainHead;

    public string GetInstructions() => string.Join(", ",  _instructions);
    
    // Helpers
    private void BindItemCommands()
    {
        AddInstruction(ConsoleKey.D1, "Inventory (1-9)");
        
        BindKey(ConsoleKey.E, new PickUpCommand(), "Pick up (E)");
        BindKey(ConsoleKey.Q, new InitDropCommand(), "Drop (Q)");
        
        BindKey(ConsoleKey.L, new InitEquipCommand(EquipmentSlot.LeftHand), "Equip/Unequip (L/R)");
        BindKey(ConsoleKey.R, new InitEquipCommand(EquipmentSlot.RightHand));
    }
    
    private void BindKey(ConsoleKey key, ICommand command, string? instruction = null, bool unique = true)
    {
        if (unique && !_keyBinds.Add(key)) return; // Already bound

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

    private void AddInstruction(ConsoleKey key, string instruction, bool unique = true)
    {
        if ((unique && !_keyBinds.Add(key)) || instruction.IsWhiteSpace()) return;
        _instructions.Add(instruction);
    }

    private void Reset()
    {
        _keyBinds.Clear();
        _instructions.Clear();
        
        BindKey(ConsoleKey.Escape, new ExitCommand(), "Quit (Esc)");
        AddInstruction(ConsoleKey.Backspace, "Return (Bck)");
        
        BindKey(ConsoleKey.W, new MoveCommand(0, -1), "Move (WASD)");
        BindKey(ConsoleKey.A, new MoveCommand(-1, 0));
        BindKey(ConsoleKey.S, new MoveCommand(0, 1));
        BindKey(ConsoleKey.D, new MoveCommand(1, 0));

        BindKey(ConsoleKey.J, new ShowJournalCommand(), "Journal (J)");
        
        BindItemCommands();
    }
}