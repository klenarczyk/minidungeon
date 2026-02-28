using MiniDungeon.World;

namespace MiniDungeon.Core;

// Temporary renderer to view the board state.
public class Renderer
{
    public static void Init()
    {
        Console.Clear();
    }
    
    public static void Draw(GameSession session)
    {
        Console.SetCursorPosition(0, 0);
        
        for (var i = 0; i < Board.Rows; i++)
        {
            for (var j = 0; j < Board.Columns; j++)
            {
                DrawCell(session.Board.Cells[i, j]);
            }
            DrawCell(new Cell{ Type = CellType.Wall });
            Console.WriteLine();
        }
        
        for (var i = 0; i < Board.Columns + 1; i++)
            DrawCell(new Cell{ Type = CellType.Wall });
        
        var playerX = session.Player.Position.X ; 
        var playerY = session.Player.Position.Y;

        Console.SetCursorPosition(playerX, playerY);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("¶");
        Console.ResetColor();
        
        Console.SetCursorPosition(0, 21);
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
                Console.ResetColor();
                break;
            default:
                throw new ArgumentException("[ERROR] Unknown cell type.");
        }
    }
}