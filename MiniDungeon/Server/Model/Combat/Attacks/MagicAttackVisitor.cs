using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Combat.Attacks;

public class MagicAttackVisitor : IAttackVisitor
{
    public CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(1, player.Luck);
    }

    public CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(1, player.Luck);
    }

    public CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(weapon.Damage, player.Intelligence * 2);
    }

    public CombatStats Visit(IInventoryItem item, Player player)
    {
        return new CombatStats(0, player.Luck);
    }
}