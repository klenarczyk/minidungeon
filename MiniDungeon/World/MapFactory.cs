namespace MiniDungeon.World;

public static class MapFactory
{
    private static string _mapText =
        """
        eeeeeeeeeeeeeeeeeeee
        ewwwwwewwwwwwewwwwwe
        eweeeweweeeeweweeewe
        eeeeeweeeeeeweeeeewe
        eweeeweweeeeweweeewe
        eweeeweweeeeweweeewe
        ewwwwwewwwwwwewwwwwe
        eeeeeeeeeeeeeeeeeeee
        eeeeeeeeeeeeeeeeeeee
        ewwwwwewwwwwwewwwwwe
        eweeeweweeeeweweeewe
        eweeeeeweeeeeeweeeee
        eweeeweweeeeweweeewe
        eweeeweweeeeweweeewe
        ewwwwwewwwwwwewwwwwe
        eeeeeeeeeeeeeeeeeeee
        eeeeeeeeeeeeeeeeeeee
        ewwwwwewwwwwwewwwwwe
        eeeeeweeeeeeweeeeewe
        eweeeweweeeeweweeewe
        eweeeweweeeeweweeewe
        eweeeweweeeeweweeewe
        ewwwwwewwwwwwewwwwwe
        eeeeeeeeeeeeeeeeeeee
        eeeeeeeeeeeeeeeeeeee
        ewwwwwewwwwwwewwwwwe
        eweeeweweeeeweweeewe
        eweeeeeweeeeeeweeeee
        eweeeweweeeeweweeewe
        eeeeeweeeeeeweeeeewe
        ewwwwwewwwwwwewwwwwe
        eeeeeeeeeeeeeeeeeeee
        eeeeeeeeeeeeeeeeeeee
        ewwwwwewwwwwwewwwwwe
        eweeeweweeeeweweeewe
        eweeeweweeeeweweeewe
        eweeeeeweeeeeeweeeee
        eweeeweweeeeweweeewe
        ewwwwwewwwwwwewwwwwe
        eeeeeeeeeeeeeeeeeeee
        """;
    
    public static Board Create()
    {
        var board = new Board();
        
        var i = 0;
        var j = 0;
        foreach (var c in _mapText)
        {
            switch (c)
            {
                case 'e':
                    board.Cells[i, j] = new Cell { Type = CellType.Empty };
                    j++;
                    break;
                case 'w':
                    board.Cells[i, j] = new Cell { Type = CellType.Wall };
                    j++;
                    break;
                case '\r':
                    break;
                case '\n':
                    i++;
                    j = 0;
                    break;
                default:
                    throw new ArgumentException($"[ERROR] Unknown map character: {c}");
            }
        }

        return board;
    }  
}