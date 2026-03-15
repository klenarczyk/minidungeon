namespace MiniDungeon.World.Generation;

public class DungeonBuilder : IDungeonBuilder
{
    private readonly Board _board = new();
    private readonly List<Room> _rooms = [];
    
    public IDungeonBuilder InitializeEmpty()
    {
        _rooms.Clear();
        
        for (var x = 0; x < Board.Columns; x++)
        for (var y = 0; y < Board.Rows; y++)
        { 
            _board[x, y] = new Cell { Type = CellType.Empty };
        }
        
        return this;
    }

    public IDungeonBuilder InitializeFilled()
    {
        _rooms.Clear();
        
        for (var x = 0; x < Board.Columns; x++)
        for (var y = 0; y < Board.Rows; y++)
        { 
            _board[x, y] = new Cell { Type = CellType.Wall };
        }
        
        return this;
    }

    public IDungeonBuilder AddCorridors()
    {
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
        
        return this;
    }

    public IDungeonBuilder AddItems(int count)
    {
        return this;
    }

    public IDungeonBuilder AddWeapons()
    {
        return this;
    }

    public Board Build() => _board;
    
    // Helpers
    private void AddRoom(Room room)
    {
        _rooms.Add(room);
        
        for (var x = room.TopLeft.X; x <= room.BottomRight.X && x < Board.Columns; x++)
        for (var y = room.TopLeft.Y; y <= room.BottomRight.Y && y < Board.Rows; y++)
        {
            _board[x, y] = new Cell { Type = CellType.Empty };
        }
    }
}