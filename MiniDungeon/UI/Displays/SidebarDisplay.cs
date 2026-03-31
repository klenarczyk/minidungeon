using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.UI.Displays;

public class SidebarDisplay : IDisplayElement
{
    private const int StartX = 43;
    private const int StartY = 0;
    private const int Width = 35; 
    private const int Height = 23;
    private const int TextX = StartX + 2;
    private const int ItemTextMaxWidth = 29;
    private const int EquipmentTextMaxWidth = 25;
    
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var player = session.Player;
        var inventory = player.Inventory;
        var cell = session.Board[player.Position];
        
        DrawFrame(buffer, inventory.Count);

        DrawPlayerInfo(buffer, player);
        DrawEquipment(buffer, player);
        DrawInventory(buffer, player);
        DrawDroppedItems(buffer, cell);
    }

    private static void DrawPlayerInfo(RenderBuffer buffer, Player player)
    {
        var purse = player.Purse;
        var attribs = player.Attributes;
        
        buffer.SetString(TextX, 1, 
            $"HP:  {attribs.Health,-3} STR: {attribs.Strength,-3} DEF: {attribs.Defense,-3}");
        buffer.SetString(TextX, 2, 
            $"INT: {attribs.Intelligence,-3} AGG: {attribs.Aggression,-3} LCK: {attribs.Luck,
            -3}");
        buffer.SetString(TextX, 4, 
            $"Gold: {purse.Gold,-3} Coins: {purse.Coins,-3}");
    }

    private static void DrawEquipment(RenderBuffer buffer, Player player)
    {
        var equipment = player.Equipment;
        
        var left = equipment[EquipmentSlot.LeftHand] != null 
            ? equipment[EquipmentSlot.LeftHand]!.Name.Length > EquipmentTextMaxWidth
                ? $"{equipment[EquipmentSlot.LeftHand]!.Name[..(EquipmentTextMaxWidth - 5)]}..."
                : equipment[EquipmentSlot.LeftHand]!.Name
            : "(Empty)";
        
        var right = equipment[EquipmentSlot.RightHand] != null 
            ? equipment[EquipmentSlot.LeftHand] != equipment[EquipmentSlot.RightHand]
                ? equipment[EquipmentSlot.RightHand]!.Name.Length > EquipmentTextMaxWidth
                    ? $"{equipment[EquipmentSlot.RightHand]!.Name[..(EquipmentTextMaxWidth - 5)]}..."
                    : equipment[EquipmentSlot.RightHand]!.Name
                : "(In use)" 
            : "(Empty)";
        
        buffer.SetString(TextX, 6, $"Left:  {left}");
        buffer.SetString(TextX, 7, $"Right: {right}");
    }
    
    private static void DrawInventory(RenderBuffer buffer, Player player)
    {
        var inventory = player.Inventory;
        
        for (var i = 0; i < inventory.Capacity; i++)
        {
            var item = i < inventory.Count 
                ? inventory.Items[i].Name.Length > ItemTextMaxWidth 
                    ? $"{inventory.Items[i].Name[..(ItemTextMaxWidth - 5)]}..."
                    : inventory.Items[i].Name
                : "-";
            
            buffer.SetString(TextX, 9 + i, $"{i + 1}. {item}");
        }
    }
    
    private static void DrawDroppedItems(RenderBuffer buffer, Cell cell)
    {
        const int a = 'a';
        for (var i = 0; i < cell.Items.Count; i++)
        {
            if (i == 3 && cell.Items.Count > 4)
            {
                buffer.SetString(TextX, 19 + i, $"... ({cell.Items.Count - 3} more)");
                break;
            }

            var item = cell.Items[i].Name.Length > ItemTextMaxWidth
                ? $"{cell.Items[i].Name[..(ItemTextMaxWidth - 5)]}..."
                : cell.Items[i].Name;
            
            buffer.SetString(TextX, 19 + i, $"{(char)(a+i)}) {item}");
        }
    }
    
    private static void DrawFrame(RenderBuffer buffer, int inventoryCount = 0)
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
        
        RendererUtils.DrawTitle(buffer, StartX, StartY, Width, " PLAYER ");
        DrawDivider(buffer, 5, " EQUIPMENT ");
        DrawDivider(buffer, 8, $" INVENTORY ({inventoryCount}/9) ");
        DrawDivider(buffer, 18, " DROPPED ");
    }

    private static void DrawDivider(RenderBuffer buffer, int y, string title)
    {
        const int endX = StartX + Width;
        
        buffer.SetChar(StartX, y, '├');
        buffer.SetChar(endX, y, '┤');
        
        for (var x = StartX + 1; x < endX; x++)
        {
            buffer.SetChar(x, y, '─');
        }

        RendererUtils.DrawTitle(buffer, StartX, y, Width, title);
    }
}