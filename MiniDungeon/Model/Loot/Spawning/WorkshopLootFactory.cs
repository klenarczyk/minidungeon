using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Modifiers;
using MiniDungeon.Model.Loot.Items.Weapons;

namespace MiniDungeon.Model.Loot.Spawning;

public class WorkshopLootFactory : LootFactory
{
    public WorkshopLootFactory()
    {
        LoadItems();
        LoadWealth();
        LoadWeapons();
        LoadModifiers();
    }
    
    private void LoadItems()
    {
        Items.Add(() => new MiscItem("scrap"));
        Items.Add(() => new MiscItem("wire"));
        Items.Add(() => new MiscItem("blueprint"));
    }
    
    private void LoadWealth()
    {
        Wealth.Add(() => new WealthItem("coin", (purse, i) => purse.TryAddCoins(i)));
        Wealth.Add(() => new WealthItem("gold", (purse, i) => purse.TryAddGold(i)));
    }
    
    private void LoadWeapons()
    {
        Weapons.Add(() => new LightWeapon("box cutter", 5));
        Weapons.Add(() => new HeavyWeapon("pipe", 14, true));
        Weapons.Add(() => new MagicWeapon("welder", 9));
    }

    private  void LoadModifiers()
    {
        Modifiers.Add(w => new DamageModifier(w, "motorized", 3));
        Modifiers.Add(w => new HealthModifier(w, "galvanized", 2));
        Modifiers.Add(w => new DamageModifier(w, "rusty", -2));
    }
}