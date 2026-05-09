using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Combat;
using MiniDungeon.Model.Combat.Attacks;
using MiniDungeon.Model.World;

namespace MiniDungeon.Model.Loot.Items.Weapons;

public class LightWeapon(string name, int damage, bool isTwoHanded = false) 
    : WeaponItem(name, damage, isTwoHanded)
{
    public override bool Collect(Player player, Cell cell, IInventoryItem item)
    {
        var res = base.Collect(player, cell, item);
        if (!res) return false;

        NoiseSubject?.Notify(cell.Position, 1);
        
        return true;
    }
    
    public override CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon) 
        => visitor.Visit(this, player, weapon);
}