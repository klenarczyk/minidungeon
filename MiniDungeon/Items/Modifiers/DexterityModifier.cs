using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Items.Modifiers;

public class DexterityModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetDexterityBonus() => WrappedWeapon.GetDexterityBonus() + bonus;
}