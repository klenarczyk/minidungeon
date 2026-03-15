using MiniDungeon.Entities;
using MiniDungeon.World;
using MiniDungeon.World.Generation;

namespace MiniDungeon.Core;

public class GameSession
{
    public bool IsRunning { get; set; } = true;
    public readonly Board Board;
    public readonly Player Player;
    public string Message { get; set; } = string.Empty;

    public GameSession()
    {
        var dungeonBuilder = new DungeonBuilder();
        var director = new DungeonDirector();
        
        Board = director.CreateStandardDungeon(dungeonBuilder);
        Player = new Player(Board.StartingPosition);
    }
}