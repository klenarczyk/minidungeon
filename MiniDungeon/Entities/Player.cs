using MiniDungeon.Components;
using MiniDungeon.World;

namespace MiniDungeon.Entities;

public class Player
{
    public Position Position { get; set; } = new();
    public Attributes Attributes { get; } = new();
    public Inventory Inventory { get; } = new();
    public Purse Purse { get; } = new();
}