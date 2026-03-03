using MiniDungeon.Items;

namespace MiniDungeon.Components;

public class Inventory(int capacity = 9)
{
    private readonly List<InventoryItem> _items = [];
    public IReadOnlyList<InventoryItem> Items => _items;
 
    public int SelectedSlot { get; set; } = -1;
    
    public int Capacity { get; } = capacity;
    public int Count => _items.Count;
    public bool HasSpace => Count < Capacity;
    
    public InventoryItem? SelectedItem => SelectedSlot >= 0 && SelectedSlot < Count ? _items[SelectedSlot] : null;
    
    public bool TryAdd(InventoryItem item)
    {
        if (!HasSpace) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryAdd(InventoryItem item, int slot)
    {
        if (!HasSpace) return false;
        if (slot < 0 || slot >= Capacity) return false;
        
        if (slot < Count)
            _items.Insert(slot, item);
        else
            _items.Add(item);
        
        return true;
    }

    public bool TryRemove(InventoryItem item)
    {
        if (!_items.Remove(item)) return false;
        
        SelectedSlot = -1;
        return true;
    }
    
    public bool TryRemove(int slot, out InventoryItem? item)
    {
        item = null;
        
        if (slot < 0 || slot >= Count) return false;
        
        item = _items[slot];
        _items.RemoveAt(slot);
        SelectedSlot = -1; 
        return true;
    }
}