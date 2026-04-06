using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Combat.Attacks;

namespace MiniDungeon.Loot.Items.Weapons;

public interface IWeaponItem : IInventoryItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
    
    CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon);
}