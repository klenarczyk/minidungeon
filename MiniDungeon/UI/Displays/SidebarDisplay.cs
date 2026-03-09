using MiniDungeon.Components;
using MiniDungeon.Core;

namespace MiniDungeon.UI.Displays;

public class SidebarDisplay : IDisplayElement
{
    private const int StartX = 43;
    private const int StartY = 0;
    private const int Width = 35; 
    private const int Height = 23; 
    
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var player = session.Player;
        var inventory = player.Inventory;
        var equipment = player.Equipment;
        var purse = player.Purse;
        var attribs = player.Attributes;
        var cell = session.Board[player.Position];
        
        DrawFrame(buffer, inventory.Count);
        
        const int textX = StartX + 2;

        // Player
        buffer.SetString(textX, 1, $"HP:  {attribs.Health,-3} STR: {attribs.Strength,-3} DEF: {attribs.Defense,-3}");
        buffer.SetString(textX, 2, $"INT: {attribs.Intelligence,-3} AGG: {attribs.Aggression,-3} LCK: {attribs.Luck,-3}");
        buffer.SetString(textX, 4, $"Gold: {purse.Gold,-3} Coins: {purse.Coins,-3}");
        
        // Equipment
        var left = equipment[EquipmentSlot.LeftHand] != null 
            ? equipment[EquipmentSlot.LeftHand]!.GetName() 
            : "(Empty)";
        
        var right = equipment[EquipmentSlot.RightHand] != null 
            ? equipment[EquipmentSlot.LeftHand] == equipment[EquipmentSlot.RightHand]
                ? "(In use)" 
                : equipment[EquipmentSlot.RightHand]!.GetName() 
            : "(Empty)";
        
        buffer.SetString(textX, 6, $"Left:  {left}");
        buffer.SetString(textX, 7, $"Right: {right}");
        
        // Inventory
        for (var i = 0; i < inventory.Capacity; i++)
        {
            var prefix = inventory.SelectedSlot == i ? "> " : "";
            
            buffer.SetString(textX, 9 + i, $"{prefix}{i + 1}. {(i < inventory.Count ? inventory.Items[i].GetName() : "-")}");
        }
        
        // Dropped Items
        const int a = 'a';
        for (var i = 0; i < cell.Items.Count; i++)
        {
            if (i == 3 && cell.Items.Count > 4)
            {
                buffer.SetString(textX, 19 + i, $"... ({cell.Items.Count - 3} more)");
                break;
            }
            
            buffer.SetString(textX, 19 + i, $"{(char)(a+i)}) {cell.Items[i].GetName()}");
        }
    }

    private void DrawFrame(RenderBuffer buffer, int inventoryCount = 0)
    {
        const int endX = StartX + Width;
        const int endY = StartY + Height;
        
        buffer.SetChar(StartX, StartY, '┌');
        buffer.SetChar(endX, StartY, '┐');
        buffer.SetChar(StartX, endY, '└');
        buffer.SetChar(endX, endY, '┘');
        
        for (var x = StartX + 1; x < endX; x++)
        {
            buffer.SetChar(x, StartY, '─');
            buffer.SetChar(x, endY, '─');
        }
        
        for (var y = StartY + 1; y < endY; y++)
        {
            buffer.SetChar(StartX, y, '│');
            buffer.SetChar(endX, y, '│');
        }
        
        DrawTitle(buffer, StartY, " PLAYER ");
        DrawDivider(buffer, 5, " EQUIPMENT ");
        DrawDivider(buffer, 8, $" INVENTORY ({inventoryCount}/9) ");
        DrawDivider(buffer, 18, " DROPPED ");
    }

    private void DrawDivider(RenderBuffer buffer, int y, string title)
    {
        const int endX = StartX + Width;
        
        buffer.SetChar(StartX, y, '├');
        buffer.SetChar(endX, y, '┤');
        
        for (var x = StartX + 1; x < endX; x++)
        {
            buffer.SetChar(x, y, '─');
        }

        DrawTitle(buffer, y, title);
    }

    private void DrawTitle(RenderBuffer buffer, int y, string title)
    {
        var startX = StartX + Width / 2 - title.Length / 2;
        for (var i = 0; i < title.Length; i++)
        {
            buffer.SetChar(startX + i, y, title[i]);
        }
    }
}