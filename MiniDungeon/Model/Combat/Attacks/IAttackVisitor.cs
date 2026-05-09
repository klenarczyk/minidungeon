using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Weapons;

namespace MiniDungeon.Model.Combat.Attacks;

public interface IAttackVisitor
{
    CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon);
    
    CombatStats Visit(IInventoryItem item, Player player);
}