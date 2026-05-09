using MiniDungeon.Controller.Input;
using MiniDungeon.Model;

namespace MiniDungeon.Controller;

public interface IGameContext
{
    GameSession Session { get; }
    void PushInputChain(IHandler chainHead, string inputMode = "");
    void PopInputChain();
}