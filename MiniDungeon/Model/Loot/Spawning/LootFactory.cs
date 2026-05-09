using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Weapons;

namespace MiniDungeon.Model.Loot.Spawning;

public abstract class LootFactory : ILootProvider
{
    protected readonly List<Func<IInventoryItem>> Items = [];
    protected readonly List<Func<IItem>> Wealth = [];
    protected readonly List<Func<IWeaponItem>> Weapons = [];
    protected readonly List<Func<IWeaponItem, IWeaponItem>> Modifiers = [];
    
    private readonly Random _random = new();

    public IItem GetRandomItem()
    {
        var recipe = Items[_random.Next(Items.Count)];
        return recipe.Invoke();
    }

    public IItem GetRandomWealth()
    {
        var recipe = Wealth[_random.Next(Wealth.Count)];
        return recipe.Invoke();
    }

    public IWeaponItem GetRandomWeapon()
    {
        var maxModifiers = Math.Min(3, Modifiers.Count);
        
        var recipe = Weapons[_random.Next(Weapons.Count)];
        var weapon = recipe.Invoke();
        
        var modifiersAdded = 0;
        while (modifiersAdded < maxModifiers)
        {
            if (_random.Next(3) > 0) break;
            
            var modifier = Modifiers[_random.Next(Modifiers.Count)];
            weapon = modifier.Invoke(weapon);
            modifiersAdded++;
        }
        
        return weapon;
    }
}