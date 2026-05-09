using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Combat;
using MiniDungeon.Server.Model.Combat.Attacks;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Model.Loot.Items.Weapons;

public class MagicWeapon(string name, int damage, bool isTwoHanded = false) 
    : WeaponItem(name, damage, isTwoHanded)
{
    public override bool Collect(Player player, Cell cell, IInventoryItem item)
    {
        var res = base.Collect(player, cell, item);
        if (!res) return false;

        NoiseSubject?.Notify(cell.Position, 3);
        
        return true;
    }
    
    public override CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon) 
        => visitor.Visit(this, player, weapon);
}