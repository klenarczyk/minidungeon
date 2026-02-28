using MiniDungeon.Components;

namespace MiniDungeon.Items.Wealth;

public class CoinItem() : WealthItem("coin")
{
    public override void AddToPurse(Purse purse, int amount = 1)
    {
        purse.TryAddCoins(amount);
    }
}