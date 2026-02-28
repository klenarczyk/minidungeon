using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Core;

public class GameSession
{
    public readonly Board Board = MapFactory.Create();
    public readonly Player Player = new();
}