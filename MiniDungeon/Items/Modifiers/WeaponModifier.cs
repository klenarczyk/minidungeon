using MiniDungeon.Items.Weapons;

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
    
    public override int Damage => WrappedWeapon.Damage + damageBonus;
    public override int GetHealthBonus() => WrappedWeapon.GetHealthBonus() + healthBonus;
    public override int GetStrengthBonus() => WrappedWeapon.GetStrengthBonus() + strengthBonus;
    public override int GetDefenseBonus() => WrappedWeapon.GetDefenseBonus() + defenseBonus;
    public override int GetIntelligenceBonus() => WrappedWeapon.GetIntelligenceBonus() + intelligenceBonus;
    public override int GetDexterityBonus() => WrappedWeapon.GetDexterityBonus() + aggressionBonus;
    public override int GetLuckBonus() => WrappedWeapon.GetLuckBonus() + luckBonus;
}