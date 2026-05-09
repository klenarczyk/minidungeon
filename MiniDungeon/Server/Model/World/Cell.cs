using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Loot.Items;
using ConsoleColor = MiniDungeon.Client.View.ConsoleColor;

namespace MiniDungeon.Server.Model.World;

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