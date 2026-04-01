using MiniDungeon.Input;

namespace MiniDungeon.Core;

public interface IGameContext
{
    GameSession Session { get; }
    void PushInputChain(IHandler chainHead, string inputMode = "");
    void PopInputChain();
}