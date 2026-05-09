using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Combat;
using MiniDungeon.Model.Combat.Attacks;
using MiniDungeon.Model.World.Systems;

namespace MiniDungeon.Model.Loot.Items.Weapons;

public interface IWeaponItem : IInventoryItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
    INoiseSubject? NoiseSubject { get; set; }
    
    CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon);
}