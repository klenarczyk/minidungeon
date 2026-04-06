using MiniDungeon.Combat;
using MiniDungeon.Entities;

namespace MiniDungeon.Items.Abstractions;

public interface IWeaponItem : IInventoryItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
    
    CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon);
}