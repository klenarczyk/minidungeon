using MiniDungeon.World;

namespace MiniDungeon.Engine.UI.Displays;

public class BoardDisplay : IDisplayElement
{
    private const int StartX = 15;
    private const int StartY = 0;
    private const int Width = Board.Columns + 2;
    
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var board = session.Board;
        var playerPos = session.Player.Position;
        
        DrawFrame(buffer);
        
        for (var y = 0; y < Board.Rows; y++)
        for (var x = 0; x < Board.Columns; x++)
        {
            var boardCell = board[x, y];
            var displayInfo = boardCell.DisplayInfo;
            buffer.SetChar(StartX + x + 1, StartY + y + 1, displayInfo.Character, displayInfo.Color);
        }
        
        buffer.SetChar(StartX + playerPos.X + 1, StartY + playerPos.Y + 1, '¶', ConsoleColor.Magenta);
    }

    private static void DrawFrame(RenderBuffer buffer)
    {
        buffer.SetChar(StartX, StartY, '┌');
        buffer.SetChar(StartX + Board.Columns + 1, StartY, '┐');
        buffer.SetChar(StartX, StartY + Board.Rows + 1, '└');
        buffer.SetChar(StartX + Board.Columns + 1, StartY + Board.Rows + 1, '┘');
        
        for (var x = 1; x <= Board.Columns; x++)
        {
            buffer.SetChar(StartX + x, StartY, '─');
            buffer.SetChar(StartX + x, StartY + Board.Rows + 1, '─');
        }
        
        for (var y = 1; y <= Board.Rows; y++)
        {
            buffer.SetChar(StartX, StartY + y, '│');
            buffer.SetChar(StartX + Board.Columns + 1, StartY + y, '│');
        }

        DrawTitle(buffer, StartX, StartY, Width, " BOARD ");
    }
    
    private static void DrawTitle(RenderBuffer buffer, int startX, int y, int width, string title)
    {
        var x = startX + width / 2 - title.Length / 2;
        for (var i = 0; i < title.Length; i++)
        {
            buffer.SetChar(x + i, y, title[i]);
        }
    }
}