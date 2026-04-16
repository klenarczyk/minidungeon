using MiniDungeon.Actors.Spawning;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Spawning;
using MiniDungeon.World.Generation;
using MiniDungeon.World.Generation.Templates;

namespace MiniDungeon.World.Themes;

public interface IDungeonTheme
{
    string EntryMessage { get; }
    IGenerationTemplate GenerationTemplate { get; }
    
    ILootProvider LootProvider { get; }
    IEnemyProvider EnemyProvider { get; }
    
    IItem CreateArtifact();
}