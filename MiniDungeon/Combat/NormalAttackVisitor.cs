using MiniDungeon.Entities;
using MiniDungeon.Items;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.Combat;

public class NormalAttackVisitor : IAttackVisitor
{
    public CombatStats Visit(HeavyWeapon weapon, Player player)
    {
        return new CombatStats(weapon.Damage, player.Strength + player.Luck);
    }

    public CombatStats Visit(LightWeapon weapon, Player player)
    {
        return new CombatStats(weapon.Damage, player.Dexterity + player.Luck);
    }

    public CombatStats Visit(MagicWeapon weapon, Player player)
    {
        return new CombatStats(1, player.Dexterity + player.Luck);
    }

    public CombatStats Visit(InventoryItem item, Player player)
    {
        return new CombatStats(0, player.Dexterity);
    }
}