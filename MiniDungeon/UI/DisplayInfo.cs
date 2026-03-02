namespace MiniDungeon.UI;

public readonly struct DisplayInfo(char character, ConsoleColor color = ConsoleColor.White)
{
    public char Character { get; } = character;
    public ConsoleColor Color { get; } = color;
}