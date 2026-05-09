using MiniDungeon.Server.Model.Actors.Spawning;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Modifiers;
using MiniDungeon.Server.Model.Loot.Items.Weapons;
using MiniDungeon.Server.Model.Loot.Spawning;
using MiniDungeon.Server.Model.World.Generation.Templates;

namespace MiniDungeon.Server.Model.World.Themes;

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