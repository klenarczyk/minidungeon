using MiniDungeon.Actors.Enemies.Behaviors;
using MiniDungeon.Combat;
using MiniDungeon.Engine;
using MiniDungeon.World;
using MiniDungeon.World.Systems;

namespace MiniDungeon.Actors.Enemies;

public class EnemyEntity(
    string name, 
    int health, 
    int attack, 
    int armor, 
    ISpeciesBehavior speciesBehavior,
    ISpeciesSubject speciesNotifier,
    INoiseSubject? noiseNotifier
    ) : IEntity
{
    public string Name { get; } = name;
    public Position Position { get; set; }
    
    private Position? TargetPosition { get; set; }
    private Queue<Position> Path { get; } = new();

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
    public void Update(INoiseSubject noise)
    {
        if (!noise.Area.Contains(Position)) return;
        TargetPosition = noise.Origin;
        Path.Clear();
    }

    public void Die(GameSession session)
    {
        speciesNotifier.Notify();
        speciesNotifier.Detach(this);
        noiseNotifier?.Detach(this);
        session.Board[Position].Entity = null;
        session.Entities.Remove(this);
    }

    public void Move(IGameContext context)
    {
        if (TargetPosition == null) RandomMove(context);
        else MoveToTarget(context);
    }
    
    private void RandomMove(IGameContext context)
    {
        var random = new Random();

        switch (random.Next(0, 6)) // 66% chance to try to move
        {
            case 0: Move(context, Position with { Y = Position.Y + 1 }); break;
            case 1: Move(context, Position with { X = Position.X + 1 }); break;
            case 2: Move(context, Position with { X = Position.X - 1 }); break;
            case 3: Move(context, Position with { Y = Position.Y - 1 }); break;
        }
    }

    private void MoveToTarget(IGameContext context)
    {
        if (TargetPosition == null) return;

        if (Path.Count == 0)
        {
            CalculatePath(Position, TargetPosition.Value, context.Session.Board);

            if (Path.Count == 0)
            {
                TargetPosition = null;
                return;
            }
        }

        var nextPos = Path.Peek();
        var hasMoved = Move(context, nextPos);
        if (hasMoved) Path.Dequeue();

        if (Position == TargetPosition)
        {
            TargetPosition = null;
            Path.Clear();
        }
    }
    
    private bool Move(IGameContext context, Position position)
    {
        var session = context.Session;
        var board = session.Board;

        if (!IsValidMove(position, board)) return false;
        
        if (position == session.Player.Position)
        {
            var cmd = new InitBattleCommand(board[Position]);
            cmd.Execute(context);
            return false;
        }
        
        var oldPos = Position;
        Position = position;
        
        board[oldPos].Entity = null;
        board[Position].Entity = this;

        return true;
    }

    private bool IsValidMove(Position position, Board board)
    {
        if (position.X < 0 || position.X > Board.Columns - 1 ||
            position.Y < 0 || position.Y > Board.Rows - 1) return false;

        if (board[position].Type == CellType.Wall ||
            board[position].Entity != null) return false;

        return true;
    }

    /// <summary>
    /// Calculates path to target using BFS
    /// </summary>
    private void CalculatePath(Position start, Position end, Board board)
    {
        Path.Clear();
        var queue = new Queue<Position>();
        var prev = new Dictionary<Position, Position>();
        
        queue.Enqueue(start);
        prev[start] = start;

        var dx = new[] { 0, 0, -1, 1 };
        var dy = new[] { -1, 1, 0, 0 };

        var found = false;
        while (queue.Count > 0)
        {
            var currPos = queue.Dequeue();

            if (currPos == end)
            {
                found = true;
                break;
            }

            for (var i = 0; i < 4; i++)
            {
                var x = currPos.X + dx[i];
                var y = currPos.Y + dy[i];
                var nextPos = new Position(x, y);
                
                if (x < 0 || x >= Board.Columns || y < 0 || y >= Board.Rows) continue;
                if (prev.ContainsKey(nextPos)) continue;
                if (board[nextPos].Type == CellType.Wall) continue;
                
                prev[nextPos] = currPos;
                queue.Enqueue(nextPos);
            }
        }

        if (found)
        {
            var curr = end;
            var path = new List<Position>();

            while (curr != start)
            {
                path.Add(curr);
                curr = prev[curr];
            }
            
            path.Reverse();

            foreach (var pos in path)
            {
                Path.Enqueue(pos);
            }
        }
    }
}