using MiniDungeon.Model.Actors.Spawning;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Modifiers;
using MiniDungeon.Model.Loot.Items.Weapons;
using MiniDungeon.Model.Loot.Spawning;
using MiniDungeon.Model.World.Generation.Templates;

namespace MiniDungeon.Model.World.Themes;

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