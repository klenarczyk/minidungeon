using MiniDungeon.Actors.Spawning;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Modifiers;
using MiniDungeon.Loot.Items.Weapons;
using MiniDungeon.Loot.Spawning;
using MiniDungeon.World.Generation;
using MiniDungeon.World.Generation.Templates;

namespace MiniDungeon.World.Themes;

public class TreasuryTheme : IDungeonTheme
{
    public string EntryMessage => "The mountain of gold is shifting beneath you...";
    public IGenerationTemplate GenerationTemplate => new WorkshopTemplate();

    public ILootProvider LootProvider { get; } = new TreasuryLootFactory();
    public IEnemyProvider EnemyProvider { get; } = new TreasuryEnemyFactory();
    
    public IItem CreateArtifact() => new HealthModifier(
        new MagicWeapon("Glove of Midas", 6),
        "",
        20);
}