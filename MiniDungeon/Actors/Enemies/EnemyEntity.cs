using MiniDungeon.Actors.Enemies.Behaviors;
using MiniDungeon.Combat;
using MiniDungeon.Engine;
using MiniDungeon.World;

namespace MiniDungeon.Actors.Enemies;

public class EnemyEntity(
    string name, 
    int health, 
    int attack, 
    int armor, 
    ISpeciesBehavior speciesBehavior,
    ISpeciesSubject speciesNotifier
    ) : IEntity, ISpeciesObserver
{
    public string Name { get; } = name;
    public Position Position { get; set; }

    public int Health { get; private set; } = health;
    public bool IsDead => Health <= 0;

    public int AttackDmg { get; set; } = attack;
    public int Armor { get; set; } = armor;

    public int TakeDamage(int damage)
    {
        var realDmg = Math.Max(0, damage - Armor);
        Health -= realDmg;
        return realDmg;
    }

    public int DealDamage()
    {
        var random = new Random();

        if (random.Next(10) == 0) // Critical hit
        {
            return (int)(AttackDmg * 1.5);
        }

        return AttackDmg;
    }

    public void Update(ISpeciesSubject species) => speciesBehavior.Act(this);

    public void Die(GameSession session)
    {
        speciesNotifier.Notify();
        speciesNotifier.Detach(this);
        session.Board[Position].Entity = null;
        session.Entities.Remove(this);
    }

    public void RandomMove(IGameContext context)
    {
        var random = new Random();

        switch (random.Next(0, 6)) // 66% chance to try to move
        {
            case 0:
                Move(context, new Position(Position.X, Position.Y + 1));
                break;
            case 1:
                Move(context, new Position(Position.X + 1, Position.Y));
                break;
            case 2:
                Move(context, new Position(Position.X - 1, Position.Y));
                break;
            case 3:
                Move(context, new Position(Position.X, Position.Y - 1));
                break;
        }
    }
    
    public void Move(IGameContext context, Position position)
    {
        var session = context.Session;
        var board = session.Board;

        if (!IsValidMove(position, board)) return;
        
        if (position == session.Player.Position)
        {
            var cmd = new InitBattleCommand(board[Position]);
            cmd.Execute(context);
            return;
        }
        
        var oldPos = Position;
        Position = position;
        
        board[oldPos].Entity = null;
        board[Position].Entity = this;
    }

    private bool IsValidMove(Position position, Board board)
    {
        if (position.X < 0 || position.X > Board.Columns - 1 ||
            position.Y < 0 || position.Y > Board.Rows - 1) return false;

        if (board[position].Type == CellType.Wall ||
            board[position].Entity != null) return false;

        return true;
    }
}