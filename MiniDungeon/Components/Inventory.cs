using MiniDungeon.Items;

namespace MiniDungeon.Components;

public class Inventory(int capacity = 9)
{
    private readonly List<InventoryItem> _items = [];
    public IReadOnlyList<InventoryItem> Items => _items;
    
    public int Capacity { get; } = capacity;
    public int Count => _items.Count;
    public bool HasSpace => Count < Capacity;
    
    public bool TryAdd(InventoryItem item)
    {
        if (!HasSpace) return false;
        
        _items.Add(item);
        return true;
    }

    public bool TryRemove(InventoryItem item) => _items.Remove(item);
}