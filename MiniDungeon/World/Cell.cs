using MiniDungeon.Items;

namespace MiniDungeon.World;

public enum CellType
{
    Empty,
    Wall
}

public class Cell
{
    public CellType Type { get; init; }
    
    private readonly List<Item> _items = [];
    public IReadOnlyList<Item> Items => _items;
    
    public bool TryAddItem(Item item)
    {
        if (Type == CellType.Wall) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryRemoveItem(Item item) => _items.Remove(item);
}