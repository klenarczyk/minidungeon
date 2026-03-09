namespace MiniDungeon.UI;

public static class RendererUtils
{
    public static void DrawTitle(RenderBuffer buffer, int startX, int y, int width, string title)
    {
        var x = startX + width / 2 - title.Length / 2;
        for (var i = 0; i < title.Length; i++)
        {
            buffer.SetChar(x + i, y, title[i]);
        }
    }
}