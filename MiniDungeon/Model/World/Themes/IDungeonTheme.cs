using MiniDungeon.Model.Actors.Spawning;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Spawning;
using MiniDungeon.Model.World.Generation.Templates;

namespace MiniDungeon.Model.World.Themes;

public interface IDungeonTheme
{
    string EntryMessage { get; }
    IGenerationTemplate GenerationTemplate { get; }
    
    ILootProvider LootProvider { get; }
    IEnemyProvider EnemyProvider { get; }
    
    IItem CreateArtifact();
}