using MiniDungeon.Items.Abstractions;
using MiniDungeon.Items.Modifiers;
using MiniDungeon.Items.Weapons;
using MiniDungeon.Providers;

namespace MiniDungeon.Items.Factories;

public class WeaponFactory : IWeaponProvider
{
    private readonly List<Func<IWeaponItem>> _weapons = [];
    private readonly List<Func<IWeaponItem, IWeaponItem>> _modifiers = [];
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
        _weapons.Add(() => new MagicWeapon("wand", 6));
        _weapons.Add(() => new LightWeapon("sword", 5));
        _weapons.Add(() => new HeavyWeapon("hammer", 9, true));
    }

    private void LoadModifiers()
    {
        _modifiers.Add(w => new WeaponModifier(w, "sharp", damageBonus: 3));
        _modifiers.Add(w => new WeaponModifier(w, "defensive", defenseBonus: 2));
        _modifiers.Add(w => new WeaponModifier(w, "unlucky", luckBonus: -2));
    }
}