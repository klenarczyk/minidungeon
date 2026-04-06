using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Combat.Attacks;

namespace MiniDungeon.Loot.Items.Weapons;

public class MagicWeapon(string name, int damage, bool isTwoHanded = false) 
    : WeaponItem(name, damage, isTwoHanded)
{
    public override CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon) 
        => visitor.Visit(this, player, weapon);
}