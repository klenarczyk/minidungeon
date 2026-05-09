using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Combat;
using MiniDungeon.Server.Model.Combat.Attacks;
using MiniDungeon.Server.Model.World.Systems;

namespace MiniDungeon.Server.Model.Loot.Items.Weapons;

public interface IWeaponItem : IInventoryItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
    INoiseSubject? NoiseSubject { get; set; }
    
    CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon);
}