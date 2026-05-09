using MiniDungeon.Model.Loot.Items.Weapons;

namespace MiniDungeon.Model.Loot.Items.Modifiers;

public class StrengthModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetStrengthBonus() => WrappedWeapon.GetStrengthBonus() + bonus;
}