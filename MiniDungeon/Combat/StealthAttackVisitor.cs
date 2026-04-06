using MiniDungeon.Entities;
using MiniDungeon.Items;
using MiniDungeon.Items.Abstractions;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.Combat;

public class StealthAttackVisitor : IAttackVisitor
{
    public CombatStats Visit(HeavyWeapon weapon, Player player)
    {
        return new CombatStats((int)(weapon.Damage * 0.5), player.Strength);
    }

    public CombatStats Visit(LightWeapon weapon, Player player)
    {
        return new CombatStats(weapon.Damage * 2, player.Dexterity);
    }

    public CombatStats Visit(MagicWeapon weapon, Player player)
    {
        return new CombatStats(1, 0);
    }

    public CombatStats Visit(IInventoryItem item, Player player)
    {
        return new CombatStats(0, 0);
    }
}