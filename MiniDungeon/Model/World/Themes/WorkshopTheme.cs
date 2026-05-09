using MiniDungeon.Model.Actors.Spawning;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Weapons;
using MiniDungeon.Model.Loot.Spawning;
using MiniDungeon.Model.World.Generation.Templates;

namespace MiniDungeon.Model.World.Themes;

public class WorkshopTheme : IDungeonTheme
{
    public string EntryMessage => "You hear the sound of grinding gears...";
    public IGenerationTemplate GenerationTemplate => new WorkshopTemplate();

    public ILootProvider LootProvider { get; } = new WorkshopLootFactory();
    public IEnemyProvider EnemyProvider { get; } = new WorkshopEnemyFactory();

    public IItem CreateArtifact() => new LightWeapon("Plasma Ray", 12);
}