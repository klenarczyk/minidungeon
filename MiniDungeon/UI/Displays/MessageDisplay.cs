using MiniDungeon.Core;
using MiniDungeon.World;

namespace MiniDungeon.UI.Displays;

public class MessageDisplay : IDisplayElement
{
    private const int StartX = 1;
    private const int StartY = 23;

    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var message = $">> {session.Message}".PadRight(Board.Columns + 40);

        buffer.SetString(StartX, StartY, message);
    }
}