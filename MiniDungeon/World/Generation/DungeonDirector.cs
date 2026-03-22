namespace MiniDungeon.World.Generation;

public class DungeonDirector
{
    public void CreateStandardDungeon(IDungeonBuilder builder)
    {
        builder
            .InitializeFilled()
            .AddCentralRoom(10, 5)
            .AddRooms()
            .AddCorridors()
            .AddItems(20)
            .AddWeapons();
    }
}