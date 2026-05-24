using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.World;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Server.Model;

public class GameSession(Board board)
{
    public readonly Board Board = board;
    public Dictionary<int, Player> Players { get; } = new();
    public List<IEntity> Entities { get; init; } = [];

    public List<ActionType> ActionList { get; init; } = [];
    public string EntryMessage { get; init; } = string.Empty;
}