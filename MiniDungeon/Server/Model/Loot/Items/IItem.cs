using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Model.Loot.Items;

public interface IItem
{
    string Name { get; }
    bool Collect(Player player, Cell cell);
}