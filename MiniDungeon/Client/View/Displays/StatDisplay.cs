using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.State;

namespace MiniDungeon.Client.View.Displays;

public class StatDisplay : IDisplayElement
{
    private const int StartX = 0;
    private const int StartY = 0;
    private const int MaxWidth = 14;
    
    public void Draw(RenderBuffer buffer, IClientContext context, GameStateDto gameState)
    {
        var player = gameState.Player;
        var attr = player.Attributes;
        var currentY = StartY;

        DrawLine($"Player {player.PlayerId}:");
        DrawLine($"  HP:  {attr.Health}/{attr.MaxHealth}");
        DrawLine($"  STR: {attr.Strength,-3}");
        DrawLine($"  DEF: {attr.Defense,-3}");
        DrawLine($"  INT: {attr.Intelligence,-3}");
        DrawLine($"  DEX: {attr.Dexterity,-3}");
        DrawLine($"  LCK: {attr.Luck,-3}");
        SkipLine();
        
        DrawLine("PURSE:");
        DrawLine($"  Gold:  {player.Purse.Gold,-3}");
        DrawLine($"  Coins: {player.Purse.Coins,-3}");

        return;
        
        void DrawLine(string text)
            => buffer.SetString(StartX, currentY++, text.PadRight(MaxWidth));
        void SkipLine() => currentY++;
    }
}