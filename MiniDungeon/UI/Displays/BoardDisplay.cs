using MiniDungeon.Core;
using MiniDungeon.World;

namespace MiniDungeon.UI.Displays;

public class BoardDisplay : IDisplayElement
{
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var board = session.Board;
        var playerPos = session.Player.Position;
        
        DrawFrame(buffer);
        
        for (var y = 0; y < Board.Rows; y++)
        {
            for (var x = 0; x < Board.Columns; x++)
            {
                var boardCell = board[x, y];
                var displayCell = boardCell.Type switch
                {
                    CellType.Empty => boardCell.Items.Count > 0 
                        ? new DisplayInfo(boardCell.Items[0].GetName()[0]) 
                        : new DisplayInfo(' '),
                    CellType.Wall => new DisplayInfo('█', ConsoleColor.Magenta),
                    _ => new DisplayInfo(' ')
                };
                buffer.SetChar(x + 1, y + 1, displayCell.Character, displayCell.Color);
            }
        }
        
        buffer.SetChar(playerPos.X + 1, playerPos.Y + 1, '¶', ConsoleColor.Magenta);
    }

    private static void DrawFrame(RenderBuffer buffer)
    {
        buffer.SetChar(0, 0, '┌');
        buffer.SetChar(Board.Columns + 1, 0, '┐');
        buffer.SetChar(0, Board.Rows + 1, '├');
        buffer.SetChar(Board.Columns + 1, Board.Rows + 1, '┤');
        
        for (var x = 1; x <= Board.Columns; x++)
        {
            buffer.SetChar(x, 0, '─');
            buffer.SetChar(x, Board.Rows + 1, '─');
        }
        
        for (var y = 1; y <= Board.Rows; y++)
        {
            buffer.SetChar(0, y, '│');
            buffer.SetChar(Board.Columns + 1, y, '│');
        }
        
        const string title = " BOARD ";
        var startX = Board.Columns / 2 - title.Length / 2 + 1; 

        for (var i = 0; i < title.Length; i++)
        {
            buffer.SetChar(startX + i, 0, title[i]);
        }
    }
}