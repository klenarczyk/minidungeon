using MiniDungeon.Server.Model;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.World;
using MiniDungeon.Shared.DTOs.Commands;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server.Controller.Commands.World;

public class MoveCommand(MoveArgs args) : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        var session = context.Session;
        var player = context.Player;
        var x = player.Position.X + args.DeltaX;
        var y = player.Position.Y + args.DeltaY;

        if (player.IsBattling) return false;
        
        if (IsEnemy(session, x, y, out var enemy) && enemy != null)
        {
            if (enemy.BattledPlayerId != null && enemy.BattledPlayerId != player.Id) return false;

            player.IsBattling = true;
            enemy.BattledPlayerId = player.Id;
            Journal.Instance.Log($"Attacked {enemy.Name}.", player.Id, context.PlayerLogs);
            return false;
        } 
        
        if (!IsValidMove(session, x, y))
        {
            Journal.Instance.Log("Bumped into a wall.", player.Id, context.PlayerLogs);
            return false;
        }

        player.Position = new Position(x, y);
        return true;
    }

    private static bool IsEnemy(GameSession session, int x, int y, out IEntity? entity)
    {
        entity = null;
        if (x is < 0 or >= Board.Columns ||
            y is < 0 or >= Board.Rows) return false;
               
        entity = session.Board[x, y].Entity;
        return entity != null;
    }
    
    private static bool IsValidMove(GameSession session, int x, int y)
    {
        return x is >= 0 and < Board.Columns && 
               y is >= 0 and < Board.Rows && 
               session.Board[x, y].Type != CellType.Wall;
    }
}