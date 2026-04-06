using MiniDungeon.Entities;
using MiniDungeon.Items;
using MiniDungeon.Items.Abstractions;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.Combat;

public interface IAttackVisitor
{
    CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon);
    
    CombatStats Visit(IInventoryItem item, Player player);
}