using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Combat.Attacks;
using MiniDungeon.World.Systems;

namespace MiniDungeon.Loot.Items.Weapons;

public interface IWeaponItem : IInventoryItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
    INoiseSubject? NoiseSubject { get; set; }
    
    CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon);
}