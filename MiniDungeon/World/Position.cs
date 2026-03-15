namespace MiniDungeon.World;

public readonly struct Position(int x = 0, int y = 0)
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;
}