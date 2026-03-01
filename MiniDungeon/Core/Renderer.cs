using MiniDungeon.World;

namespace MiniDungeon.Core;

// Temporary renderer to view the board state.
public class Renderer
{
    public static void Init()
    {
        Console.CursorVisible = false;
        Console.Clear();
    }
    
    public static void Draw(GameSession session)
    {
        Console.Clear();
        
        for (var i = 0; i < Board.Rows; i++)
        {
            for (var j = 0; j < Board.Columns; j++)
            {
                DrawCell(session.Board[j, i]);
            }
            DrawCell(new Cell{ Type = CellType.Wall });
            Console.WriteLine();
        }
        
        for (var i = 0; i < Board.Columns + 1; i++)
            DrawCell(new Cell{ Type = CellType.Wall });
        Console.WriteLine();

        for (var i = 0; i < session.Player.Inventory.Count; i++)
        {
            Console.WriteLine($"{i}. {session.Player.Inventory.Items[i].Name}");
        }
        
        Console.WriteLine(session.Message);
        
        var playerX = session.Player.Position.X ; 
        var playerY = session.Player.Position.Y;

        Console.SetCursorPosition(playerX, playerY);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("¶");
        Console.ResetColor();
    }

    private static void DrawCell(Cell cell)
    {
        switch (cell.Type)
        {
            case CellType.Empty:
                if (cell.Items.Count == 0)
                    Console.Write(" ");
                else 
                    Console.Write(cell.Items[0].Name[0]);
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