using MiniDungeon.Combat;
using MiniDungeon.Components;
using MiniDungeon.Entities;
using MiniDungeon.Items.Abstractions;
using MiniDungeon.Items.Weapons;
using MiniDungeon.World;

namespace MiniDungeon.Items.Modifiers;

public abstract class WeaponDecorator(IWeaponItem weapon) : IWeaponItem
{
    protected readonly IWeaponItem WrappedWeapon = weapon;
    
    public virtual string Name => WrappedWeapon.Name;
    public virtual int Damage => WrappedWeapon.Damage;
    public virtual bool IsTwoHanded => WrappedWeapon.IsTwoHanded;
    
    public bool OnPickup(Player player, Cell cell)
    {
        throw new NotImplementedException();
    }

    public bool OnEquip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand)
    {
        throw new NotImplementedException();
    }

    public virtual int GetHealthBonus() => WrappedWeapon.GetHealthBonus();
    public virtual int GetStrengthBonus() => WrappedWeapon.GetStrengthBonus();
    public virtual int GetDefenseBonus() => WrappedWeapon.GetDefenseBonus();
    public virtual int GetIntelligenceBonus() => WrappedWeapon.GetIntelligenceBonus();
    public virtual int GetDexterityBonus() => WrappedWeapon.GetDexterityBonus();
    public virtual int GetLuckBonus() => WrappedWeapon.GetLuckBonus();

    public virtual CombatStats Accept(IAttackVisitor visitor, Player player) 
        => WrappedWeapon.Accept(visitor, player);
}