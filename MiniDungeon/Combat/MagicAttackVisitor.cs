using MiniDungeon.Entities;
using MiniDungeon.Items;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.Combat;

public class MagicAttackVisitor : IAttackVisitor
{
    public CombatStats Visit(HeavyWeapon weapon, Player player)
    {
        return new CombatStats(1, player.Luck);
    }

    public CombatStats Visit(LightWeapon weapon, Player player)
    {
        return new CombatStats(1, player.Luck);
    }

    public CombatStats Visit(MagicWeapon weapon, Player player)
    {
        return new CombatStats(weapon.Damage, player.Intelligence * 2);
    }

    public CombatStats Visit(InventoryItem item, Player player)
    {
        return new CombatStats(0, player.Luck);
    }
}