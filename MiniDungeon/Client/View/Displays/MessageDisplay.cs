using MiniDungeon.Server.Model.World;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Client.View.Displays;

public class MessageDisplay : IDisplayElement
{
    private const int StartX = 1;
    private const int StartY = 23;

    public void Draw(RenderBuffer buffer, GameStateDto gameState)
    {
        var prefix = gameState.InputMode == string.Empty
            ? string.Empty
            : $"({gameState.InputMode}) ";
        
        var message = $"{prefix}>> {gameState.Message}".PadRight(Board.Columns + 40);
        buffer.SetString(StartX, StartY, message);
    }
}