using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.State;

namespace MiniDungeon.Client.View.Displays;

public class SidebarDisplay : IDisplayElement
{
    private const int StartX = 58;
    private const int StartY = 0;
    private const int MaxWidth = 60;
    
    public void Draw(RenderBuffer buffer, IClientContext context, GameStateDto gameState)
    {
        var player = gameState.Player;
        List<ItemDto> cellItems = [];

        foreach (var item in gameState.Items)
        {
            if (item.X == player.X && item.Y == player.Y) cellItems.Add(item);
        }

        var currentY = StartY;

        DrawLine("EQUIPMENT:");
        var left = player.Equipment.Left?.Name ?? "-";
        var right = player.Equipment.Right?.Name ?? "-";
        
        if (player.Equipment.Left is { IsTwoHanded: true })
        {
            right = "(In use)";
        }
        
        DrawLine($"  L: {left}");
        DrawLine($"  R: {right}");
        SkipLine();
        
        var inv = player.Inventory;
        DrawLine($"INVENTORY: ({inv.Count}/{inv.Capacity})");
        for (var i = 0; i < inv.Capacity; i++)
        {
            var item = i < inv.Count ? inv.Items[i].Name : "-";
            DrawLine($"  {i + 1}. {item}");
        }
        SkipLine();
        
        DrawLine("DROPPED:");
        if (cellItems.Count == 0)
        {
            DrawLine("  -");
        }
        else
        {
            for (var i = 0; i < cellItems.Count; i++)
            {
                if (i == 3 && cellItems.Count > 5)
                {
                    DrawLine($"  ... ({cellItems.Count - 3} more)");
                    break;
                }
            
                DrawLine($"  {(char)('a' + i)}) {cellItems[i].Name}");
            }
        }

        return;

        void DrawLine(string text)
            => buffer.SetString(StartX, currentY++, text.PadRight(MaxWidth));
        void SkipLine() => currentY++;
    }
}