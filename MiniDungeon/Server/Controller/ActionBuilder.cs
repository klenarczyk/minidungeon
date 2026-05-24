using MiniDungeon.Server.Controller.Actions;
using MiniDungeon.Server.Controller.Commands.Combat;
using MiniDungeon.Server.Model.World.Generation;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Server.Controller;

public class ActionBuilder : IDungeonBuilder
{
    public List<ActionType> ActionList { get; } = [];

    private IEnvelopeHandler? _commandChainHead;
    private IEnvelopeHandler? _commandChainTail;

    public ActionBuilder() => Reset();

    public IEnvelopeHandler? GetCommandChain() => _commandChainHead;
    
    public IDungeonBuilder InitializeEmpty() { Reset(); return this; }
    public IDungeonBuilder InitializeFilled() { Reset(); return this; }

    public IDungeonBuilder AddCorridors() => this;
    public IDungeonBuilder AddRooms() => this;
    public IDungeonBuilder AddCentralRoom(int width, int height) => this;
    public IDungeonBuilder AddItems(int count) => this;
    public IDungeonBuilder AddWeapons() => this;

    public IDungeonBuilder AddEnemies()
    {
        AddHandler(new AttackParser(), ActionType.Attack); 
        AddHandler(new FleeParser(), ActionType.Flee);
        return this;
    }
    
    // ---
    
    private void BindItemCommands()
    {
        AddHandler(new PickupParser(), ActionType.Pickup);
        AddHandler(new DropParser(), ActionType.Drop);
        AddHandler(new EquipParser(), ActionType.Equip);
    }

    private void Reset()
    {
        ActionList.Clear();
        
        AddHandler(new ExitParser(), ActionType.Exit);
        AddHandler(new MoveParser(), ActionType.Move);

        AddAction(ActionType.Journal);
        
        BindItemCommands();
    }
    
    // ---
    
    private bool AddAction(ActionType action)
    {
        if (ActionList.Contains(action)) return false;
        
        ActionList.Add(action);
        return true;
    }

    private void AddHandler(IEnvelopeHandler handler, ActionType? action = null)
    {
        if (action != null && !AddAction(action.Value)) return;

        if (_commandChainHead == null || _commandChainTail == null)
        {
            _commandChainHead = handler;
            _commandChainTail = handler;
        }
        else
        {
            _commandChainTail = _commandChainTail.SetNext(handler);
        }
    }
}