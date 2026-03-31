namespace MiniDungeon.Items.Modifiers;

public class WeaponModifier(
    WeaponItem weapon,
    string suffix,
    int damageBonus = 0,
    int healthBonus = 0,
    int strengthBonus = 0,
    int defenseBonus = 0,
    int intelligenceBonus = 0,
    int aggressionBonus = 0,
    int luckBonus = 0
    ) : WeaponDecorator(weapon)
{
    public override string Name => $"{WrappedWeapon.Name} ({suffix})";
    
    public override int Damage => Math.Max(1, WrappedWeapon.Damage + damageBonus);
    public override int GetHealthBonus() => Math.Max(1, WrappedWeapon.GetHealthBonus() + healthBonus);
    public override int GetStrengthBonus() => Math.Max(1, WrappedWeapon.GetStrengthBonus() + strengthBonus);
    public override int GetDefenseBonus() => Math.Max(1, WrappedWeapon.GetDefenseBonus() + defenseBonus);
    public override int GetIntelligenceBonus() => Math.Max(1, WrappedWeapon.GetIntelligenceBonus() + intelligenceBonus);
    public override int GetAggressionBonus() => Math.Max(1, WrappedWeapon.GetAggressionBonus() + aggressionBonus);
    public override int GetLuckBonus() => Math.Max(1, WrappedWeapon.GetLuckBonus() + luckBonus);
}