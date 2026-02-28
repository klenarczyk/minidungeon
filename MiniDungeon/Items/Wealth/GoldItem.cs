using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items.Wealth;

public class GoldItem() : Item("gold")
{
    public override bool OnPickup(Player player, Cell cell)
    {
        if (!cell.TryRemoveItem(this)) return false;
        
        player.Purse.TryAddGold(1);
        return true;
    }
}