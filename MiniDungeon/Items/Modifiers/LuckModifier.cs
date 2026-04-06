using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Items.Modifiers;

public class LuckModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetLuckBonus() => WrappedWeapon.GetLuckBonus() + bonus;
}