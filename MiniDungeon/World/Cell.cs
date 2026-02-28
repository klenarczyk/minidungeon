namespace MiniDungeon.World;

public enum CellType
{
    Empty,
    Wall
}

public class Cell
{
    public CellType Type { get; init; }
}