using MiniDungeon.Items;

namespace MiniDungeon.Providers;

public interface IWealthProvider
{
    IItem GetRandomWealth();
}