using MiniDungeon.Server.Model.World;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.State;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Client.View.Displays;

public class MessageDisplay : IDisplayElement
{
    private const int StartX = 1;
    private const int StartY = 23;

    public void Draw(RenderBuffer buffer, IClientContext context, GameStateDto gameState)
    {
        var prefix = context.InputMode == string.Empty
            ? string.Empty
            : $"({context.InputMode}) ";
        
        var msg = Journal.Instance.Entries.Count > 0
            ? Journal.Instance.Entries[^1]
            : context.EntryMessage;
        
        var message = $"{prefix}>> {msg}".PadRight(Board.Columns + 40);
        buffer.SetString(StartX, StartY, message);
    }
}