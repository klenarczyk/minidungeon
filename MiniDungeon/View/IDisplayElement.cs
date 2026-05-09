using MiniDungeon.Model;

namespace MiniDungeon.View;

public interface IDisplayElement
{
    void Draw(RenderBuffer buffer, GameSession session);
}