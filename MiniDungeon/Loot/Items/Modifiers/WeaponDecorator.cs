using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Combat.Attacks;
using MiniDungeon.Loot.Items.Weapons;
using MiniDungeon.World;

namespace MiniDungeon.Loot.Items.Modifiers;

public abstract class WeaponDecorator(IWeaponItem weapon, string suffix) : IWeaponItem
{
    protected readonly IWeaponItem WrappedWeapon = weapon;

    public string Name => $"{WrappedWeapon.Name} ({suffix})";
    public virtual int Damage => WrappedWeapon.Damage;
    public bool IsTwoHanded => WrappedWeapon.IsTwoHanded;

    public bool Collect(Player player, Cell cell) => WrappedWeapon.Collect(player, cell, this);
    public bool Collect(Player player, Cell cell, IInventoryItem item) => WrappedWeapon.Collect(player, cell, item);

    public bool Equip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand)
     => WrappedWeapon.Equip(player, slot);

    public virtual int GetHealthBonus() => WrappedWeapon.GetHealthBonus();
    public virtual int GetStrengthBonus() => WrappedWeapon.GetStrengthBonus();
    public virtual int GetDefenseBonus() => WrappedWeapon.GetDefenseBonus();
    public virtual int GetIntelligenceBonus() => WrappedWeapon.GetIntelligenceBonus();
    public virtual int GetDexterityBonus() => WrappedWeapon.GetDexterityBonus();
    public virtual int GetLuckBonus() => WrappedWeapon.GetLuckBonus();

    public CombatStats Accept(IAttackVisitor visitor, Player player) 
        => WrappedWeapon.Accept(visitor, player, this);

    public CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon)
        => WrappedWeapon.Accept(visitor, player, weapon);
}