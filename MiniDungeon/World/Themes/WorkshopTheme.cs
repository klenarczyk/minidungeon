using MiniDungeon.Actors.Spawning;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Modifiers;
using MiniDungeon.Loot.Items.Weapons;
using MiniDungeon.Loot.Spawning;
using MiniDungeon.World.Generation;

namespace MiniDungeon.World.Themes;

public class WorkshopTheme : IDungeonTheme
{
    public string EntryMessage => "You hear the sound of grinding gears...";
    public void GenerateDungeon(IDungeonBuilder builder)
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

    public ILootProvider LootProvider { get; } = new WorkshopLootFactory();
    public IEnemyProvider EnemyProvider { get; } = new WorkshopEnemyFactory();

    public IItem CreateArtifact() => new LightWeapon("Plasma Ray", 12);
}