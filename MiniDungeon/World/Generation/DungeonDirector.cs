namespace MiniDungeon.World.Generation;

public class DungeonDirector
{
    public Board CreateStandardDungeon(IDungeonBuilder builder)
    {
        return builder
            .InitializeFilled()
            .AddCentralRoom(10, 5)
            .AddRooms()
            .AddItems(30)
            .AddWeapons()
            .Build();
    }
}