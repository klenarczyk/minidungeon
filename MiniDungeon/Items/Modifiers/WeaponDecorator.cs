namespace MiniDungeon.Items.Modifiers;

public abstract class WeaponDecorator(WeaponItem weapon)
    : WeaponItem(weapon.Name, weapon.Damage, weapon.IsTwoHanded)
{
    protected readonly WeaponItem WrappedWeapon = weapon;
    
    public override string Name => WrappedWeapon.Name;
    public override int Damage => WrappedWeapon.Damage;
    public override int GetHealthBonus() => WrappedWeapon.GetHealthBonus();
    public override int GetStrengthBonus() => WrappedWeapon.GetStrengthBonus();
    public override int GetDefenseBonus() => WrappedWeapon.GetDefenseBonus();
    public override int GetIntelligenceBonus() => WrappedWeapon.GetIntelligenceBonus();
    public override int GetAggressionBonus() => WrappedWeapon.GetAggressionBonus();
    public override int GetLuckBonus() => WrappedWeapon.GetLuckBonus();
}