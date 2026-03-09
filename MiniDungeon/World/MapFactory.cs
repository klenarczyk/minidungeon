using MiniDungeon.Items;
using MiniDungeon.Items.Misc;
using MiniDungeon.Items.Wealth;
using MiniDungeon.Items.Weapons;

namespace MiniDungeon.World;

public static class MapFactory
{
    private const string MapText = """
                                   ...##.............................##....
                                   .................##...............##....
                                   ......###........####..............##...
                                   .#######...........................##...
                                   .##..........#####....###...........##..
                                   .##........#######....###...........##..
                                   ###........#####......###..........##...
                                   .....................##.................
                                   .......######.............#####.........
                                   ........######...........########.......
                                   ........######.............#######......
                                   .......######...............######......
                                   ........................................
                                   ...................................##...
                                   ...###..........###....###.........####.
                                   ...###..........###....####.........###.
                                   ...###...........##......##.........###.
                                   .................####...................
                                   .........###......###.......###.........
                                   ........####...............#####........
                                   """;

    public static Board Create()
    {
        var board = new Board();
        
        var i = 0;
        var j = 0;
        foreach (var c in MapText)
        {
            switch (c)
            {
                case '.':
                    board[j, i] = new Cell { Type = CellType.Empty };
                    if (Random.Shared.NextDouble() < 0.3) 
                        AddRandomItem(board[j, i]);
                    j++;
                    break;
                case '#':
                    board[j, i] = new Cell { Type = CellType.Wall };
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

    private static void AddRandomItem(Cell cell)
    {
        
        var rand = Random.Shared.Next() % 8;
        IItem item = rand switch
        {
            0 => new SwordItem(),
            1 => new AxeItem(),
            2 => new HammerItem(),
            3 => new StickItem(),
            4 => new RockItem(),
            5 => new ClothItem(),
            6 => new GoldItem(),
            _ => new CoinItem()
        };

        cell.TryAddItem(item);
    }
}