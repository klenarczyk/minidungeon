using MiniDungeon.Core;

namespace MiniDungeon.UI;

public interface IDisplayElement
{
    void Draw(RenderBuffer buffer, GameSession session);
}