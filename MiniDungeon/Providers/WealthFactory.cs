using MiniDungeon.Items;

namespace MiniDungeon.Providers;

public class WealthFactory : IWealthProvider
{
    private readonly List<Func<IItem>> _wealth = [];
    private readonly Random _random = new();
    
    public WealthFactory()
    {
        _wealth.Add(() => new WealthItem("coin", (purse, i) => purse.TryAddCoins(i)));
        _wealth.Add(() => new WealthItem("gold", (purse, i) => purse.TryAddGold(i)));
    }
    
    public IItem GetRandomWealth()
    {
        var recipe = _wealth[_random.Next(_wealth.Count)];
        return recipe.Invoke();
    }
}