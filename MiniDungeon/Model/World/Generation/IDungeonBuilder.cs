namespace MiniDungeon.Model.World.Generation;

public interface IDungeonBuilder
{
    IDungeonBuilder InitializeEmpty();
    IDungeonBuilder InitializeFilled();
    IDungeonBuilder AddCorridors();
    IDungeonBuilder AddRooms();
    IDungeonBuilder AddCentralRoom(int width, int height);
    IDungeonBuilder AddItems(int count);
    IDungeonBuilder AddWeapons();
    IDungeonBuilder AddEnemies();
}