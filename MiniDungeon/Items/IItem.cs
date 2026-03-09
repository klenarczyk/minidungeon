using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items;

public interface IItem
{
    string GetName();
    bool OnPickup(Player player, Cell cell);
}