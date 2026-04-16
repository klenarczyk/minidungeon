namespace MiniDungeon.World.Generation.Templates;

public class LibraryTemplate : IGenerationTemplate
{
    public void Use(IDungeonBuilder builder)
    {
        builder
            .InitializeFilled()
            .AddRooms()
            .AddCorridors()
            .AddItems(20)
            .AddWeapons()
            .AddEnemies();
    }
}