using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Loot.Items.Modifiers;

public class LuckModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int GetLuckBonus() => WrappedWeapon.GetLuckBonus() + bonus;
}