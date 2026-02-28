using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items;

public abstract class Item(string name)
{
    public string Name { get; } = name;
    
    public abstract bool OnPickup(Player player, Cell cell);
}