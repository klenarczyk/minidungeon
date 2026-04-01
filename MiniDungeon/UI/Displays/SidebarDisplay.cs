using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.UI.Displays;

public class SidebarDisplay : IDisplayElement
{
    private const int StartX = 58;
    private const int StartY = 0;
    private const int MaxWidth = 60;
    
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var player = session.Player;
        var inventory = player.Inventory;
        var cell = session.Board[player.Position];

        var currentY = StartY;

        DrawLine("EQUIPMENT:");
        var left = player.Equipment[EquipmentSlot.LeftHand]?.Name ?? "-";
        var right = player.Equipment[EquipmentSlot.RightHand]?.Name ?? "-";
        
        if (left != "-" && player.Equipment[EquipmentSlot.LeftHand] == player.Equipment[EquipmentSlot.RightHand])
        {
            right = "(In use)";
        }
        
        DrawLine($"  L: {left}");
        DrawLine($"  R: {right}");
        SkipLine();
        
        DrawLine($"INVENTORY: ({inventory.Count}/{inventory.Capacity})");
        for (var i = 0; i < inventory.Capacity; i++)
        {
            var item = i < inventory.Count ? inventory.Items[i].Name : "-";
            DrawLine($"  {i + 1}. {item}");
        }
        SkipLine();
        
        DrawLine("DROPPED:");
        if (cell.Items.Count == 0)
        {
            DrawLine("  -");
        }
        else
        {
            for (var i = 0; i < cell.Items.Count; i++)
            {
                if (i == 3 && cell.Items.Count > 5)
                {
                    DrawLine($"  ... ({cell.Items.Count - 3} more)");
                    break;
                }
            
                DrawLine($"  {(char)('a' + i)}) {cell.Items[i].Name}");
            }
        }

        return;

        void DrawLine(string text)
            => buffer.SetString(StartX, currentY++, text.PadRight(MaxWidth));
        void SkipLine() => currentY++;
    }
}