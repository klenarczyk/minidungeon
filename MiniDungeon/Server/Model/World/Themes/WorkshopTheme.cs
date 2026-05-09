using MiniDungeon.Server.Model.Actors.Spawning;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Weapons;
using MiniDungeon.Server.Model.Loot.Spawning;
using MiniDungeon.Server.Model.World.Generation.Templates;

namespace MiniDungeon.Server.Model.World.Themes;

public class WorkshopTheme : IDungeonTheme
{
    public string EntryMessage => "You hear the sound of grinding gears...";
    public IGenerationTemplate GenerationTemplate => new WorkshopTemplate();

    public ILootProvider LootProvider { get; } = new WorkshopLootFactory();
    public IEnemyProvider EnemyProvider { get; } = new WorkshopEnemyFactory();

    public IItem CreateArtifact() => new LightWeapon("Plasma Ray", 12);
}