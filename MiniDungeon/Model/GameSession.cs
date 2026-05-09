using MiniDungeon.Model.Actors;
using MiniDungeon.Model.World;

namespace MiniDungeon.Model;

public class GameSession(Board board)
{
    public bool IsRunning { get; set; } = true;
    public readonly Board Board = board;
    public readonly List<Player> Players = [];
    public List<IEntity> Entities { get; init; } = [];
    public string Message { get; set; } = string.Empty;
    public string InputMode { get; set; } = string.Empty;
    public bool ShowJournal { get; set; } = false;
}