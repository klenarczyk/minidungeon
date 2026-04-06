using MiniDungeon.Actors;
using MiniDungeon.World;

namespace MiniDungeon.Loot.Items;

public interface IItem
{
    string Name { get; }
    bool Collect(Player player, Cell cell);
}