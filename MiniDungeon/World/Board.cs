namespace MiniDungeon.World;

public class Board
{
    public const int Rows = 20;
    public const int Columns = 40;
    public Position StartingPosition { get; set; }
    
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

    public List<Cell> GetFreeCells()
    {
        var freeCells = new List<Cell>();
        
        for (var x = 0; x < Columns; x++)
        for (var y = 0; y < Rows; y++)
        {
            if (this[x, y].Type == CellType.Empty)
                freeCells.Add(this[x, y]);
        }

        return freeCells;
    }
}