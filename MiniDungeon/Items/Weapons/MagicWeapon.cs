using MiniDungeon.Combat;
using MiniDungeon.Entities;

namespace MiniDungeon.Items.Weapons;

public class MagicWeapon(string name, int damage, bool isTwoHanded = false) 
    : WeaponItem(name, damage, isTwoHanded)
{
    public override CombatStats Accept(IAttackVisitor visitor, Player player) 
        => visitor.Visit(this, player);
}