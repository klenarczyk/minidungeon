using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Items.Modifiers;

public class HealthModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetHealthBonus() => WrappedWeapon.GetHealthBonus() + bonus;
}