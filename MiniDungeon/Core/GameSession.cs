using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Core;

public class GameSession
{
    public bool IsRunning { get; set; } = true;
    public readonly Board Board = MapFactory.Create();
    public readonly Player Player = new();
    public string Message { get; set; } = string.Empty;
}