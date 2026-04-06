using MiniDungeon.Entities;
using MiniDungeon.Items;
using MiniDungeon.Items.Abstractions;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.Combat;

public interface IAttackVisitor
{
    CombatStats Visit(HeavyWeapon weapon, Player player);
    CombatStats Visit(LightWeapon weapon, Player player);
    CombatStats Visit(MagicWeapon weapon, Player player);
    
    CombatStats Visit(IInventoryItem item, Player player);
}