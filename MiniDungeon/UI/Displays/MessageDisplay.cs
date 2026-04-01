using MiniDungeon.Core;
using MiniDungeon.World;

namespace MiniDungeon.UI.Displays;

public class MessageDisplay : IDisplayElement
{
    private const int StartX = 1;
    private const int StartY = 23;

    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var prefix = session.InputMode == string.Empty
            ? string.Empty
            : $"({session.InputMode}) ";
        
        var message = $"{prefix}>> {session.Message}".PadRight(Board.Columns + 40);
        buffer.SetString(StartX, StartY, message);
    }
}