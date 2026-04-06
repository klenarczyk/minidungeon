using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items.Abstractions;

public interface IItem
{
    string Name { get; }
    bool Collect(Player player, Cell cell);
}