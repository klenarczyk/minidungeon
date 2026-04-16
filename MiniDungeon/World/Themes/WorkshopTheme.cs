using MiniDungeon.Actors.Spawning;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Modifiers;
using MiniDungeon.Loot.Items.Weapons;
using MiniDungeon.Loot.Spawning;
using MiniDungeon.World.Generation;
using MiniDungeon.World.Generation.Templates;

namespace MiniDungeon.World.Themes;

public class WorkshopTheme : IDungeonTheme
{
    public string EntryMessage => "You hear the sound of grinding gears...";
    public IGenerationTemplate GenerationTemplate => new WorkshopTemplate();

    public ILootProvider LootProvider { get; } = new WorkshopLootFactory();
    public IEnemyProvider EnemyProvider { get; } = new WorkshopEnemyFactory();

    public IItem CreateArtifact() => new LightWeapon("Plasma Ray", 12);
}