using MiniDungeon.Components;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items;

public abstract class WealthItem(string name) : Item(name)
{
    public abstract void AddToPurse(Purse purse, int amount = 1);
    
    public override bool OnPickup(Player player, Cell cell)
    {
        if (!cell.TryRemoveItem(this)) return false;
        
        AddToPurse(player.Purse);
        return true;
    }
}