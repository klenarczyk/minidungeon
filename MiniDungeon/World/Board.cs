namespace MiniDungeon.World;

public class Board
{
    public const int Rows = 20;
    public const int Columns = 40;
    
    private readonly Cell[,] _cells = new Cell[Rows, Columns];

    public Cell this[int x, int y]
    {
        get => _cells[y, x];
        set => _cells[y, x] = value;
    }

    public Cell this[Position position]
    {
        get => _cells[position.Y, position.X];
        set => _cells[position.Y, position.X] = value;
    }
    
}