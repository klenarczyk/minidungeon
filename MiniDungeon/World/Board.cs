namespace MiniDungeon.World;

public class Board
{
    public const int Rows = 20;
    public const int Columns = 40;
    
    public readonly Cell[,] Cells = new Cell[Rows, Columns];
}