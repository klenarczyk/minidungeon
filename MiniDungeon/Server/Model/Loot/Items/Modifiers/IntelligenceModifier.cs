using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Loot.Items.Modifiers;

public class IntelligenceModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetIntelligenceBonus() => WrappedWeapon.GetIntelligenceBonus() + bonus;
}