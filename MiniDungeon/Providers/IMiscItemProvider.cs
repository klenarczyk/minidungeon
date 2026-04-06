using MiniDungeon.Items;
using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Providers;

public interface IMiscItemProvider
{
    IItem GetRandomItem();
}