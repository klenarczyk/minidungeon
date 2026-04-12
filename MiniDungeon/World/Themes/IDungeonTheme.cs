using MiniDungeon.Actors.Spawning;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Spawning;
using MiniDungeon.World.Generation;

namespace MiniDungeon.World.Themes;

public interface IDungeonTheme
{
    string EntryMessage { get; }
    void GenerateDungeon(IDungeonBuilder builder);
    
    ILootProvider LootProvider { get; }
    IEnemyProvider EnemyProvider { get; }
    
    IItem CreateArtifact();
}