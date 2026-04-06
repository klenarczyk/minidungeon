namespace MiniDungeon.Engine.UI;

public interface IDisplayElement
{
    void Draw(RenderBuffer buffer, GameSession session);
}