using MiniDungeon.Components;
using MiniDungeon.World;

namespace MiniDungeon.Entities;

public class Player(Position startingPosition)
{
    public Position Position { get; set; } = startingPosition;
    public Attributes Attributes { get; } = new();
    public Inventory Inventory { get; } = new();
    public Equipment Equipment { get; } = new();
    public Purse Purse { get; } = new();
}