using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Model;

public class GameSession(Board board)
{
    public bool IsRunning { get; set; } = true;
    public readonly Board Board = board;

    public Dictionary<int, Player> Players { get; } = new();
    public List<IEntity> Entities { get; init; } = [];
    
    public string EntryMessage { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}