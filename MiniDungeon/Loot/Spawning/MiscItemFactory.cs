using MiniDungeon.Loot.Items;

namespace MiniDungeon.Loot.Spawning;

public class MiscItemFactory : IMiscItemProvider
{
    private readonly List<Func<IInventoryItem>> _items = [];
    private readonly Random _random = new();
    
    public MiscItemFactory()
    {
        _items.Add(() => new MiscItem("stick"));
        _items.Add(() => new MiscItem("rock"));
        _items.Add(() => new MiscItem("cloth"));
    }
    
    public IItem GetRandomItem()
    {
        var recipe = _items[_random.Next(_items.Count)];
        return recipe.Invoke();
    }
}