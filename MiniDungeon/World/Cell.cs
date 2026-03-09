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
    
    private readonly List<IItem> _items = [];
    public IReadOnlyList<IItem> Items => _items;
    
    public bool TryAddItem(IItem item)
    {
        if (Type == CellType.Wall) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryRemoveItem(IItem item) => _items.Remove(item);
}