namespace MiniDungeon.Model.World.Systems;

public class NoiseNotifier(Board board) : INoiseSubject
{
    private readonly List<INoiseObserver> _observers = [];
    
    private readonly List<Position> _area = [];
    public IReadOnlyList<Position> Area => _area;
    public Position Origin => _area.First();
    
    public void Attach(INoiseObserver observer)
    {
        if (_observers.Contains(observer)) return;
        _observers.Add(observer);
    }

    public void Detach(INoiseObserver observer) => _observers.Remove(observer);

    public void Notify(Position origin, int maxDistance)
    {
        SetArea(origin, maxDistance);
        
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    /// <summary>
    /// Finds the positions reachable by the noise using BFS
    /// </summary>
    private void SetArea(Position origin, int maxDistance)
    {
        _area.Clear();
        
        var queue = new Queue<(Position pos, int distance)>();
        var visited = new HashSet<Position>();

        queue.Enqueue((origin, 0));
        visited.Add(origin);
        
        var dx = new[] { 0, 0, -1, 1 };
        var dy = new[] { -1, 1, 0, 0 };

        while (queue.Count > 0)
        {
            var (currPos, currDist) = queue.Dequeue();
            _area.Add(currPos);
            
            if (currDist >= maxDistance) continue;

            for (var i = 0; i < 4; i++)
            {
                var x = currPos.X + dx[i];
                var y = currPos.Y + dy[i];
                var nextPos = new Position(x, y);
                
                if (x < 0 || x >= Board.Columns || y < 0 || y >= Board.Rows) continue;
                if (visited.Contains(nextPos)) continue;
                if (board[nextPos].Type == CellType.Wall) continue;
                
                visited.Add(nextPos);
                queue.Enqueue((nextPos, currDist + 1));
            }
        }
    }
}