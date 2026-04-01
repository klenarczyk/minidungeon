using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Core;

public class GameSession(Board board)
{
    public bool IsRunning { get; set; } = true;
    public readonly Board Board = board;
    public readonly Player Player = new(board.StartingPosition);
    public string Message { get; set; } = string.Empty;
    public string InputMode { get; set; } = string.Empty;
}