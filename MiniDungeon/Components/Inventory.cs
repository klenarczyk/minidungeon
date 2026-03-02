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
    
    public bool TryAdd(InventoryItem item)
    {
        if (!HasSpace) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryAddAt(InventoryItem item, int slot)
    {
        if (!HasSpace) return false;
        if (slot < 0 || slot >= Capacity) return false;
        
        if (slot < Count)
            _items.Insert(slot, item);
        else
            _items.Add(item);
        
        return true;
    }
    
    public bool TryRemove(InventoryItem item) => _items.Remove(item);
    
    public bool TryRemoveAt(int slot, out InventoryItem? item)
    {
        item = null;
        
        if (slot < 0 || slot >= Count) return false;
        
        item = _items[slot];
        if (TryRemove(item))
        {
            SelectedSlot = -1;
            return true;
        };

        return false;
    }
}