using MiniDungeon.Loot.Items;

namespace MiniDungeon.Loot.Spawning;

public interface IWealthProvider
{
    IItem GetRandomWealth();
}