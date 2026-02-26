namespace MiniDungeon.World;

public class Board
{
    public const int Rows = 40;
    public const int Columns = 20;
    
    public readonly Cell[,] Cells = new Cell[Rows, Columns];
}