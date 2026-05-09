using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Combat.Attacks;

public class StealthAttackVisitor : IAttackVisitor
{
    public CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats((int)(weapon.Damage * 0.5), player.Strength);
    }

    public CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(weapon.Damage * 2, player.Dexterity);
    }

    public CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon)
    {
        return new CombatStats(1, 0);
    }

    public CombatStats Visit(IInventoryItem item, Player player)
    {
        return new CombatStats(0, 0);
    }
}