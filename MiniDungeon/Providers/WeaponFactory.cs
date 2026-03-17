using MiniDungeon.Items;

namespace MiniDungeon.Providers;

public class WeaponFactory : IWeaponProvider
{
    private readonly List<Func<IItem>> _weapons = [];
    private readonly Random _random = new();

    public WeaponFactory()
    {
        _weapons.Add(() => new WeaponItem("sword", 3));
        _weapons.Add(() => new WeaponItem("axe", 5));
        _weapons.Add(() => new WeaponItem("hammer", 7, true));
    }
    
    public IItem GetRandomWeapon()
    {
        var recipe = _weapons[_random.Next(_weapons.Count)];
        return recipe.Invoke();
    }
}