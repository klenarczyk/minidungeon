using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Items.Modifiers;

public class DefenseModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetDefenseBonus() => WrappedWeapon.GetDefenseBonus() + bonus;
}