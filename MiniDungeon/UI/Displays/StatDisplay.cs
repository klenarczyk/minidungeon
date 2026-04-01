using MiniDungeon.Components;
using MiniDungeon.Core;

namespace MiniDungeon.UI.Displays;

public class StatDisplay : IDisplayElement
{
    private const int StartX = 0;
    private const int StartY = 0;
    private const int MaxWidth = 14;
    
    public void Draw(RenderBuffer buffer, GameSession session)
    {
        var player = session.Player;
        var currentY = StartY;

        DrawLine("PLAYER:");
        DrawLine($"  HP:  {player.Health}/{player.MaxHealth}");
        DrawLine($"  STR: {player.Strength,-3}");
        DrawLine($"  DEF: {player.Defense,-3}");
        DrawLine($"  INT: {player.Intelligence,-3}");
        DrawLine($"  DEX: {player.Dexterity,-3}");
        DrawLine($"  LCK: {player.Luck,-3}");
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