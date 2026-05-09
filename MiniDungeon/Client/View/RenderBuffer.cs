using System.Text;

namespace MiniDungeon.Client.View;

public class RenderBuffer
{
    private readonly DisplayInfo[,] _buffer;
    public int Width { get; }
    public int Height { get; }
    public string Instructions { get; set; }
    
    private const string ResetColor = "\e[0m";

    public RenderBuffer(int width, int height)
    {
        Instructions = string.Empty;
        Width = width;
        Height = height;
        _buffer = new DisplayInfo[width, height];
        Clear();
    }

    public void SetChar(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return;
        _buffer[x, y] = new DisplayInfo(c, color);
    }

    public void SetString(int startX, int y, string s, ConsoleColor color = ConsoleColor.White)
    {
        if (startX < 0 || startX + s.Length > Width || y < 0 || y >= Height) return;
        for (var i = 0; i < s.Length; i++)
        {
            SetChar(startX + i, y, s[i], color);
        }
    }
    
    public void Clear()
    {
        for (var i = 0; i < Width; i++)
        for (var j = 0; j < Height; j++)
        {
            _buffer[i, j] = new DisplayInfo(' ');
        }
    }

    public void Draw()
    {
        var sb = new StringBuilder();
        var lastColor = ConsoleColor.White;
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (_buffer[x, y].Color != lastColor)
                {
                    sb.Append($"\e[{(int)_buffer[x, y].Color}m");
                    lastColor = _buffer[x, y].Color;
                }
                sb.Append(_buffer[x, y].Character);
            }
            sb.AppendLine();
        }
        sb.Append(ResetColor);
        
        if (!string.IsNullOrWhiteSpace(Instructions))
            sb.Append($"\n{Instructions}\n");
        
        Console.SetCursorPosition(0, 0);
        Console.Write(sb.ToString());
    }
}