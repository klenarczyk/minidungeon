using MiniDungeon.Items;

namespace MiniDungeon.Providers;

public interface IMiscItemProvider
{
    IItem GetRandomItem();
}