using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Modifiers;
using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Loot.Spawning;

public class LibraryLootFactory : LootFactory
{
    public LibraryLootFactory()
    {
        LoadItems();
        LoadWealth();
        LoadWeapons();
        LoadModifiers();
    }
    
    private void LoadItems()
    {
        Items.Add(() => new MiscItem("glasses"));
        Items.Add(() => new MiscItem("book"));
        Items.Add(() => new MiscItem("ink"));
    }
    
    private void LoadWealth()
    {
        Wealth.Add(() => new WealthItem("coin", (purse, i) => purse.TryAddCoins(i)));
    }
    
    private void LoadWeapons()
    {
        Weapons.Add(() => new LightWeapon("quill", 4));
        Weapons.Add(() => new HeavyWeapon("encyclopedia", 9, true));
        Weapons.Add(() => new MagicWeapon("wand", 6));
    }

    private  void LoadModifiers()
    {
        Modifiers.Add(w => new IntelligenceModifier(w, "wise", 3));
        Modifiers.Add(w => new LuckModifier(w, "runed", 2));
        Modifiers.Add(w => new HealthModifier(w, "forbidden", -2));
    }
}