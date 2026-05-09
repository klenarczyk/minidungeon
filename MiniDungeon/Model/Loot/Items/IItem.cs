using MiniDungeon.Model.Actors;
using MiniDungeon.Model.World;

namespace MiniDungeon.Model.Loot.Items;

public interface IItem
{
    string Name { get; }
    bool Collect(Player player, Cell cell);
}