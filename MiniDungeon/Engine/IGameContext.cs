using MiniDungeon.Engine.Input;

namespace MiniDungeon.Engine;

public interface IGameContext
{
    GameSession Session { get; }
    void PushInputChain(IHandler chainHead, string inputMode = "");
    void PopInputChain();
}