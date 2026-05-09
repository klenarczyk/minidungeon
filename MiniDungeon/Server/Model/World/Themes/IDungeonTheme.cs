using MiniDungeon.Server.Model.Actors.Spawning;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Spawning;
using MiniDungeon.Server.Model.World.Generation.Templates;

namespace MiniDungeon.Server.Model.World.Themes;

public interface IDungeonTheme
{
    string EntryMessage { get; }
    IGenerationTemplate GenerationTemplate { get; }
    
    ILootProvider LootProvider { get; }
    IEnemyProvider EnemyProvider { get; }
    
    IItem CreateArtifact();
}