using MiniDungeon.Server.Controller.Input;
using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;

namespace MiniDungeon.Server;

public interface IGameContext
{
    Player Player { get; }
    GameSession Session { get; }

    List<string> PlayerLogs { get; }
    bool ShowJournal { get; set; }
    
    void PushInputChain(IHandler chainHead, string inputMode = "");
    void PopInputChain();
}