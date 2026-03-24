using MiniDungeon.Core;
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

        if (!IsValidMove(session, x, y)) return;
        player.Position = new Position(x, y);
    }
    
    private static bool IsValidMove(GameSession session, int x, int y)
    {
        return x is >= 0 and < Board.Columns && 
               y is >= 0 and < Board.Rows && 
               session.Board[x, y].Type != CellType.Wall;
    }
}