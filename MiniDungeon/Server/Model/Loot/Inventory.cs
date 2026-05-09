using MiniDungeon.Server.Model.Loot.Items;

namespace MiniDungeon.Server.Model.Loot;

public class Inventory(int capacity = 9)
{
    private readonly List<IInventoryItem> _items = [];
    public IReadOnlyList<IInventoryItem> Items => _items;
 
    public int SelectedSlot { get; set; } = -1;
    
    public int Capacity { get; } = capacity;
    public int Count => _items.Count;
    public bool HasSpace => Count < Capacity;
    
    public IInventoryItem? SelectedItem => SelectedSlot >= 0 && SelectedSlot < Count ? _items[SelectedSlot] : null;
    
    public bool TryAdd(IInventoryItem item)
    {
        if (!HasSpace) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryAdd(IInventoryItem item, int slot)
    {
        if (!HasSpace) return false;
        if (slot < 0 || slot >= Capacity) return false;
        
        if (slot < Count)
            _items.Insert(slot, item);
        else
            _items.Add(item);
        
        return true;
    }

    public bool TryRemove(IInventoryItem item)
    {
        if (!_items.Remove(item)) return false;
        
        SelectedSlot = -1;
        return true;
    }
    
    public bool TryRemove(int slot, out IInventoryItem? item)
    {
        item = null;
        
        if (slot < 0 || slot >= Count) return false;
        
        item = _items[slot];
        _items.RemoveAt(slot);
        SelectedSlot = -1; 
        return true;
    }
}