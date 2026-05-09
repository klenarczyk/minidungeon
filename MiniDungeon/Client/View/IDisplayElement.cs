using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Client.View;

public interface IDisplayElement
{
    void Draw(RenderBuffer buffer, GameStateDto gameState);
}