using MiniDungeon.Entities;
using MiniDungeon.Items.Abstractions;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.Combat;

public class NormalAttackVisitor : IAttackVisitor
{
    public CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(weapon.Damage, player.Strength + player.Luck);
    }

    public CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(weapon.Damage, player.Dexterity + player.Luck);
    }

    public CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(1, player.Dexterity + player.Luck);
    }

    public CombatStats Visit(IInventoryItem item, Player player)
    {
        return new CombatStats(0, player.Dexterity);
    }
}