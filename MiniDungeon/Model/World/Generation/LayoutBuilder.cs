using MiniDungeon.Model.Actors;
using MiniDungeon.Model.World.Systems;
using MiniDungeon.Model.World.Themes;

namespace MiniDungeon.Model.World.Generation;

public class LayoutBuilder : IDungeonBuilder
{
    private readonly Board _board = new();
    private readonly IDungeonTheme _theme;
    private readonly List<IEntity> _entityList;
    private readonly INoiseSubject _noiseSubject;
    private readonly List<Room> _rooms = [];

    public LayoutBuilder(IDungeonTheme theme, List<IEntity> entityList)
    {
        _theme = theme;
        _entityList = entityList;
        _noiseSubject = new NoiseNotifier(_board);
    }
    
    public IDungeonBuilder InitializeEmpty()
    {
        _rooms.Clear();
        _board.StartingPosition = new Position();
        
        for (var x = 0; x < Board.Columns; x++)
        for (var y = 0; y < Board.Rows; y++)
        { 
            _board[x, y] = new Cell(new Position(x, y)) { Type = CellType.Empty };
        }
        
        return this;
    }

    public IDungeonBuilder InitializeFilled()
    {
        _rooms.Clear();
        _board.StartingPosition = new Position();
        
        for (var x = 0; x < Board.Columns; x++)
        for (var y = 0; y < Board.Rows; y++)
        { 
            _board[x, y] = new Cell(new Position(x, y)) { Type = CellType.Wall };
        }
        
        return this;
    }

    public IDungeonBuilder AddCorridors()
    {
        if (_rooms.Count < 1) return this;
        
        // Prim's algo for linking rooms
        var visited = new List<Room> { _rooms[0] };
        var unvisited = _rooms.Skip(1).ToList();
        
        while (unvisited.Count > 0)
        {
            var minDist = float.MaxValue;
            Room? roomA = null;
            Room? roomB = null;

            foreach (var r1 in visited)
            foreach (var r2 in unvisited)
            {
                var dist = GetDistance(r1.Center, r2.Center);
                if (!(dist < minDist)) continue;
                minDist = dist;
                roomA = r1;
                roomB = r2;
            }

            if (roomA == null || roomB == null) continue;
            ConnectRooms(roomA.Center, roomB.Center);
                
            visited.Add(roomB);
            unvisited.Remove(roomB);
        }
        
        return this;
    }

    public IDungeonBuilder AddRooms()
    {
        var random = new Random();
        var numRooms = random.Next(5, 10);
        var attempts = 0;

        while (_rooms.Count < numRooms && attempts++ < 50)
        {
            var w = random.Next(4, 8);
            var h = random.Next(3, 6);
            var x = random.Next(1, Board.Columns - w - 1);
            var y = random.Next(1, Board.Rows - h - 1);
            
            x = Math.Max(x, 0);
            y = Math.Max(y, 0);
            
            var room =  new Room(x, y, w, h);

            if (!_rooms.Any(r => r.Intersects(room)))
            {
                AddRoom(room);

                if (_board.StartingPosition is { X: 0, Y: 0 } && _board[0, 0].Type == CellType.Wall)
                {
                    _board.StartingPosition = room.Center;
                }
            }
        }
        
        return this;
    }

    public IDungeonBuilder AddCentralRoom(int width, int height)
    {
        var x = (Board.Columns - width) / 2;
        var y = (Board.Rows - height) / 2;

        x = Math.Max(x, 0);
        y = Math.Max(y, 0);
        
        var room = new Room(x, y, width, height);
        AddRoom(room);
        
        _board.StartingPosition = room.Center;
        
        return this;
    }

    public IDungeonBuilder AddItems(int count)
    {
        var loot = _theme.LootProvider;

        var freeCells = _board.GetFreeCells();
        if (freeCells.Count == 0) return this;
        
        var random = new Random();
        for (var i = 0; i < count; i++)
        {
            var cell = freeCells[random.Next(freeCells.Count)];
            var type = random.Next(2);

            var item = (type == 0) 
                ? loot.GetRandomWealth() 
                : loot.GetRandomItem();

            cell.TryAddItem(item);
        }
        
        return this;
    }

    public IDungeonBuilder AddWeapons()
    {
        var weapon = _theme.LootProvider;

        var freeCells = _board.GetFreeCells();
        if (freeCells.Count == 0) return this;
        
        var random = new Random();
        for (var i = 0; i < random.Next(3, 10); i++)
        {
            var cell = freeCells[random.Next(freeCells.Count)];
            var item = weapon.GetRandomWeapon();
            item.NoiseSubject = _noiseSubject;
            
            cell.TryAddItem(item);
        }

        return this;
    }

    public IDungeonBuilder AddEnemies()
    {
        var enemies = _theme.EnemyProvider;
        enemies.NoiseSubject = _noiseSubject;

        var freeCells = _board.GetFreeCells();
        if (freeCells.Count <= 1) return this;

        var random = new Random();
        for (var i = 0; i < 3; i++)
        {
            var recipe = enemies.GetRandomRecipe();
            
            var attempts = 0;
            for (var j = 0; j < 3 && attempts < 20; j++)
            {
                var cell = freeCells[random.Next(freeCells.Count)];
                if (cell.Type != CellType.Wall &&
                    cell.Entity == null &&
                    cell != _board[_board.StartingPosition])
                {
                    var entity = recipe.Invoke();
                    entity.Position = cell.Position;
                    cell.Entity = entity;
                    _entityList.Add(entity);
                }
                else attempts++;
            }
        }
        
        return this;
    }

    public Board Build()
    {
        if (_board.StartingPosition is { X: 0, Y: 0 } && _board[0, 0].Type == CellType.Wall)
        {
            _board[0, 0] = new Cell(new Position()) {  Type = CellType.Empty };
        }

        var random = new Random();
        var freeCells = _board.GetFreeCells();
        var cell = freeCells[random.Next(freeCells.Count)];
        var artifact = _theme.CreateArtifact();
        cell.TryAddItem(artifact);
        
        return _board;
    }

    // Helpers
    private void AddRoom(Room room)
    {
        _rooms.Add(room);
        
        for (var x = room.TopLeft.X; x <= room.BottomRight.X && x < Board.Columns; x++)
        for (var y = room.TopLeft.Y; y <= room.BottomRight.Y && y < Board.Rows; y++)
        {
            _board[x, y] = new Cell(new Position(x, y)) { Type = CellType.Empty };
        }
    }

    private void ConnectRooms(Position roomA, Position roomB)
    {
        var random = new Random();

        var startX = Math.Min(roomA.X, roomB.X);
        var startY =  Math.Min(roomA.Y, roomB.Y);
        
        var endX = Math.Max(roomA.X, roomB.X);
        var endY =  Math.Max(roomA.Y, roomB.Y);
        
        if (random.Next(0, 1) == 0)
        {
            for (var x = startX; x <= endX; x++)
                _board[x, roomA.Y] = new Cell(new Position(x, roomA.Y)) { Type = CellType.Empty };

            for (var y = startY; y <= endY; y++)
                _board[roomB.X, y] = new Cell(new Position(roomB.X, y)) { Type = CellType.Empty };
            
            return;
        }
        
        for (var x = startX; x <= endX; x++)
            _board[x, roomA.Y] = new Cell(new Position(x, roomA.Y)) { Type = CellType.Empty };

        for (var y = startY; y <= endY; y++)
            _board[roomB.X, y] = new Cell(new Position(roomB.X, y)) { Type = CellType.Empty };
    }
    
    private float GetDistance(Position p1, Position p2)
    {
        return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }
}