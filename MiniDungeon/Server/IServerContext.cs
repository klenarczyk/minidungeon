using MiniDungeon.Server.Controller.Actions;
using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;

namespace MiniDungeon.Server;

public interface IServerContext
{
    Player Player { get; }
    GameSession Session { get; }
    bool IsRunning { get; set; }
    List<string> PlayerLogs { get; }
}