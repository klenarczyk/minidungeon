using MiniDungeon.World;

namespace MiniDungeon.Core;

// Temporary renderer to view the board state.
public class Renderer
{
    public static void Draw(GameSession session)
    {
        for (var i = 0; i < Board.Columns + 2; i++)
            DrawCell(new Cell{ Type = CellType.Wall });
        Console.WriteLine();
        
        for (var i = 0; i < Board.Rows; i++)
        {
            DrawCell(new Cell{ Type = CellType.Wall });
            for (var j = 0; j < Board.Columns; j++)
            {
                DrawCell(session.Board.Cells[i, j]);
            }
            DrawCell(new Cell{ Type = CellType.Wall });
            Console.WriteLine();
        }
        
        for (var i = 0; i < Board.Columns + 2; i++)
            DrawCell(new Cell{ Type = CellType.Wall });
    }

    private static void DrawCell(Cell cell)
    {
        switch (cell.Type)
        {
            case CellType.Empty:
                Console.Write(" ");
                break;
            case CellType.Wall:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("█");
                Console.ForegroundColor = ConsoleColor.White;
                break;
            default:
                throw new ArgumentException("[ERROR] Unknown cell type.");
        }
    }
}