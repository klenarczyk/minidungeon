using MiniDungeon.Loot.Items;

namespace MiniDungeon.Loot.Spawning;

public interface IMiscItemProvider
{
    IItem GetRandomItem();
}