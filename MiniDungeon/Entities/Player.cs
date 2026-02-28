using MiniDungeon.World;

namespace MiniDungeon.Entities;

public class Player
{
    public Position Position { get; set; } = new();
    public Attributes Attributes { get; set; } = new();
}