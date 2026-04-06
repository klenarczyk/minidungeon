using MiniDungeon.Entities;
using MiniDungeon.Items.Abstractions;
using MiniDungeon.UI;
using ConsoleColor = MiniDungeon.UI.ConsoleColor;

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
    public IEntity? Entity { get; set; }

    public DisplayInfo DisplayInfo
    {
        get
        {
            if (Type == CellType.Wall) 
                return new DisplayInfo('█', ConsoleColor.Magenta);

            if (Entity != null)
                return new DisplayInfo(Entity.Name[0], ConsoleColor.Red);

            if (Items.Count > 0)
                return new DisplayInfo(Items[0].Name[0]);
            
            return new DisplayInfo(' ');
        }
    }

    public bool TryAddItem(IItem item)
    {
        if (Type == CellType.Wall) return false;
        
        _items.Add(item);
        return true;
    }
    
    public bool TryRemoveItem(IItem item) => _items.Remove(item);
}