using MiniDungeon.Actors;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Weapons;

namespace MiniDungeon.Combat.Attacks;

public interface IAttackVisitor
{
    CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon);
    
    CombatStats Visit(IInventoryItem item, Player player);
}