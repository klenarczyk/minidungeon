using MiniDungeon.Core;
using MiniDungeon.World;

namespace MiniDungeon.UI.Displays;

public class MessageDisplay : IDisplayElement
{
    private const int StartX = 1;
    private const int StartY = 22;
    
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        DrawFrame(buffer);
        
        var message = session.Message.Length > Board.Columns - 2
            ? $"{session.Message[..(Board.Columns - 5)]}..." 
            : session.Message;
        
        buffer.SetString(StartX, StartY, $" {message}");
    }

    private static void DrawFrame(RenderBuffer buffer)
    {
        RendererUtils.DrawTitle(buffer, 0, StartY - 1, Board.Columns + 2, " MESSAGE ");
        
        buffer.SetChar(0, StartY, '│');
        buffer.SetChar(Board.Columns + 1, StartY, '│');
        for (var x = 1; x < Board.Columns + 1; x++)
        {
            buffer.SetChar(x, StartY + 1, '─');
        }
        buffer.SetChar(0, StartY + 1, '└');
        buffer.SetChar(Board.Columns + 1, StartY + 1, '┘');
    }
}