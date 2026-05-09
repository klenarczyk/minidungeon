using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Combat.Attacks;

public interface IAttackVisitor
{
    CombatStats Visit(HeavyWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(LightWeapon weaponType, Player player, IWeaponItem weapon);
    CombatStats Visit(MagicWeapon weaponType, Player player, IWeaponItem weapon);
    
    CombatStats Visit(IInventoryItem item, Player player);
}