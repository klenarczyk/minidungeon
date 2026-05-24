using MiniDungeon.Server.Model.World;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.State;

namespace MiniDungeon.Client.View.Displays;

public class BoardDisplay : IDisplayElement
{
    private const int StartX = 15;
    private const int StartY = 0;
    private const int Width = Board.Columns + 2;
    
    public void Draw(RenderBuffer buffer, IClientContext context, GameStateDto gameState)
    {
        DrawFrame(buffer);

        for (var y = 0; y < Board.Rows; y++)
        for (var x = 0; x < Board.Columns; x++)
        {
            buffer.SetChar(StartX + x + 1, StartY + y + 1, ' ');
        }
        
        foreach (var pos in gameState.Walls)
        {
            buffer.SetChar(StartX + pos.X + 1, StartY + pos.Y + 1, '█', ConsoleColor.Magenta);
        }

        foreach (var item in gameState.Items)
        {
            buffer.SetChar(StartX + item.X + 1, StartY + item.Y + 1, item.Name[0]);
        }
        
        foreach (var enemy in gameState.Enemies)
        {
            buffer.SetChar(StartX + enemy.X + 1, StartY + enemy.Y + 1, enemy.Name[0], ConsoleColor.Red);
        }

        foreach (var (id, player) in gameState.Players)
        {
            buffer.SetChar(StartX + player.X + 1, StartY + player.Y + 1, $"{id}"[0], ConsoleColor.Magenta);
        }
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