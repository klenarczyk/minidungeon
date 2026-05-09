namespace MiniDungeon.Server.Model.World.Generation.Templates;

public class WorkshopTemplate : IGenerationTemplate
{
    public void Use(IDungeonBuilder builder)
    {
        builder
            .InitializeFilled()
            .AddCentralRoom(10, 5)
            .AddRooms()
            .AddCorridors()
            .AddItems(20)
            .AddWeapons()
            .AddEnemies();
    }
}