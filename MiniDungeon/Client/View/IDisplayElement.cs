using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.State;

namespace MiniDungeon.Client.View;

public interface IDisplayElement
{
    void Draw(RenderBuffer buffer, IClientContext context, GameStateDto gameState);
}