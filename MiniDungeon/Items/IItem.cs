using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items;

public interface IItem
{
    string Name { get; }
    bool OnPickup(Player player, Cell cell);
}