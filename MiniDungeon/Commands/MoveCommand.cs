using MiniDungeon.Core;
using MiniDungeon.World;

namespace MiniDungeon.Commands;

public class MoveCommand(int deltaX = 0, int deltaY = 0) : ICommand
{
    public void Execute(GameSession session)
    {
        var player = session.Player;
        var x = player.Position.X + deltaX;
        var y = player.Position.Y + deltaY;

        if (IsValidMove(session, x, y))
        {
            player.Position.X = x;
            player.Position.Y = y;
            session.Message = string.Empty;
        }
        else
        {
            session.Message = "You can't move there!";
        }
    }
    
    private static bool IsValidMove(GameSession session, int x, int y)
    {
        return x is >= 0 and < Board.Columns && 
               y is >= 0 and < Board.Rows && 
               session.Board[x, y].Type != CellType.Wall;
    }
}