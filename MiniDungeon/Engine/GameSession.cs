using MiniDungeon.Actors;
using MiniDungeon.World;

namespace MiniDungeon.Engine;

public class GameSession(Board board)
{
    public bool IsRunning { get; set; } = true;
    public readonly Board Board = board;
    public readonly Player Player = new(board.StartingPosition);
    public List<IEntity> Entities { get; init; } = [];
    public string Message { get; set; } = string.Empty;
    public string InputMode { get; set; } = string.Empty;
    public bool ShowJournal { get; set; } = false;
}