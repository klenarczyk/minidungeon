using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Modifiers;
using MiniDungeon.Loot.Items.Weapons;

namespace MiniDungeon.Loot.Spawning;

public class TreasuryLootFactory : LootFactory
{
    public TreasuryLootFactory()
    {
        LoadItems();
        LoadWealth();
        LoadWeapons();
        LoadModifiers();
    }
    
    private void LoadItems()
    {
        Items.Add(() => new MiscItem("monocle"));
        Items.Add(() => new MiscItem("checkbook"));
        Items.Add(() => new MiscItem("coin (fake)"));
    }
    
    private void LoadWealth()
    {
        Wealth.Add(() => new WealthItem("coin", (purse, i) => purse.TryAddCoins(i)));
        Wealth.Add(() => new WealthItem("gold", (purse, i) => purse.TryAddGold(i)));
        Wealth.Add(() => new WealthItem("pouch", (purse, i) => purse.TryAddCoins(i * 2)));
    }
    
    private void LoadWeapons()
    {
        Weapons.Add(() => new LightWeapon("dagger", 5));
        Weapons.Add(() => new HeavyWeapon("chain", 9, true));
        Weapons.Add(() => new MagicWeapon("scepter", 4));
    }

    private  void LoadModifiers()
    {
        Modifiers.Add(w => new DamageModifier(w, "flawless", 3));
        Modifiers.Add(w => new LuckModifier(w, "gilded", 2));
        Modifiers.Add(w => new DexterityModifier(w, "counterfeit", -2));
    }
}