using MiniDungeon.Components;
using MiniDungeon.World;

namespace MiniDungeon.Entities;

public class Player(int startX = 0, int startY = 0)
{
    public Position Position { get; set; } = new(startX, startY);
    public Attributes Attributes { get; } = new();
    public Inventory Inventory { get; } = new();
    public Equipment Equipment { get; } = new();
    public Purse Purse { get; } = new();
}