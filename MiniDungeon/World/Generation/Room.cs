namespace MiniDungeon.World.Generation;

public class Room(int x, int y, int width, int height)
{
    public Position TopLeft => new Position(x, y);
    public Position BottomRight => new Position(x + width - 1, y + height - 1);
    public Position Center => new Position(x + width / 2, y + height / 2);

    public bool Intersects(Room other)
    {
        if (BottomRight.X < other.TopLeft.X) return false;
        if (other.BottomRight.X < TopLeft.X) return false;
        if (BottomRight.Y < other.TopLeft.Y) return false;
        if (other.BottomRight.Y < TopLeft.Y) return false;

        return true;
    }
}