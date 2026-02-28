using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items.Wealth;

public class CoinItem() : Item("coin")
{
    public override bool OnPickup(Player player, Cell cell)
    {
        if (!cell.TryRemoveItem(this)) return false;
        
        player.Purse.TryAddCoins(1);
        return true;
    }
}