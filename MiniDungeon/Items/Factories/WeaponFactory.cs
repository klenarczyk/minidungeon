using MiniDungeon.Items.Modifiers;
using MiniDungeon.Providers;

namespace MiniDungeon.Items.Factories;

public class WeaponFactory : IWeaponProvider
{
    private readonly List<Func<WeaponItem>> _weapons = [];
    private readonly List<Func<WeaponItem, WeaponItem>> _modifiers = [];
    private readonly Random _random = new();

    public WeaponFactory()
    {
        LoadWeapons();
        LoadModifiers();
    }
    
    public IItem GetRandomWeapon()
    {
        const int maxModifiers = 3;
        
        var recipe = _weapons[_random.Next(_weapons.Count)];
        var weapon = recipe.Invoke();
        
        var modifiersAdded = 0;
        while (modifiersAdded < maxModifiers)
        {
            if (_random.Next(3) > 0) break;
            
            var modifier = _modifiers[_random.Next(_modifiers.Count)];
            weapon = modifier.Invoke(weapon);
            modifiersAdded++;
        }
        
        return weapon;
    }

    private void LoadWeapons()
    {
        _weapons.Add(() => new WeaponItem("sword", 3));
        _weapons.Add(() => new WeaponItem("axe", 5));
        _weapons.Add(() => new WeaponItem("hammer", 7, true));
    }

    private void LoadModifiers()
    {
        _modifiers.Add(w => new WeaponModifier(w, "sharp", damageBonus: 5));
        _modifiers.Add(w => new WeaponModifier(w, "defensive", defenseBonus: 5));
        _modifiers.Add(w => new WeaponModifier(w, "unlucky", luckBonus: -2));
    }
}