using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Logging;

namespace MiniDungeon.World;

public class MoveCommand(int deltaX = 0, int deltaY = 0) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
        var x = player.Position.X + deltaX;
        var y = player.Position.Y + deltaY;

        if (IsEnemy(session, x, y, out var enemy) && enemy != null)
        {
            var attackCommandInit = new InitBattleCommand(session.Board[x, y]);
            return attackCommandInit.Execute(context);
        } 
        
        if (!IsValidMove(session, x, y))
        {
            Journal.Instance.Log("You bumped into a wall.");
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