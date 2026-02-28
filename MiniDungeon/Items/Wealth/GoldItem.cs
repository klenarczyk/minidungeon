using MiniDungeon.Components;

namespace MiniDungeon.Items.Wealth;

public class GoldItem() : WealthItem("gold")
{
    public override void AddToPurse(Purse purse, int amount = 1)
    {
        purse.TryAddGold(amount);
    }
}