using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Loot.Items.Modifiers;

public class DamageModifier(IWeaponItem weapon, string suffix, int bonus = 0 ) 
    : WeaponDecorator(weapon, suffix)
{
    public override int Damage => WrappedWeapon.Damage + bonus;
}