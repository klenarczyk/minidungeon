using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.View;
using ConsoleColor = MiniDungeon.View.ConsoleColor;

namespace MiniDungeon.Model.World;

public enum CellType
{
    Empty,
    Wall
}

public class Cell(Position position)
{
    public CellType Type { get; init; }
    public Position Position { get; } = position;
    
    private readonly List<IItem> _items = [];
    public IReadOnlyList<IItem> Items => _items;
    public IEntity? Entity { get; set; }

    public bool TryAddItem(IItem item)
    {
        if (Type == CellType.Wall) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryRemoveItem(IItem item) => _items.Remove(item);
}