using MiniDungeon.Providers;

namespace MiniDungeon.Items.Factories;

public class MiscItemFactory : IMiscItemProvider
{
    private readonly List<Func<MiscItem>> _items = [];
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