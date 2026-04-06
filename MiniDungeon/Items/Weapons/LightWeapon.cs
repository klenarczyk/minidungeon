using MiniDungeon.Combat;
using MiniDungeon.Entities;
using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Items.Weapons;

public class LightWeapon(string name, int damage, bool isTwoHanded = false) 
    : WeaponItem(name, damage, isTwoHanded)
{
    public override CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon) 
        => visitor.Visit(this, player, weapon);
}