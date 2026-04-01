using MiniDungeon.Core;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Commands;

public class MoveCommand(int deltaX = 0, int deltaY = 0) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
        var x = player.Position.X + deltaX;
        var y = player.Position.Y + deltaY;

        if (IsEnemy(session, x, y, out var enemy) && enemy != null)
        {
            var attackCommandInit = new InitBattleCommand(session.Board[x, y]);
            attackCommandInit.Execute(context);
        } else if (IsValidMove(session, x, y))
        {
            player.Position = new Position(x, y);
        }
        
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