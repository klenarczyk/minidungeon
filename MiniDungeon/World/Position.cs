namespace MiniDungeon.World;

public readonly struct Position(int x = 0, int y = 0)
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;
    
    public static bool operator ==(Position a, Position b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Position a, Position b) => a.X != b.X || a.Y != b.Y;
}