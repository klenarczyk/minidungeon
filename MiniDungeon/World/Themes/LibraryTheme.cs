using MiniDungeon.Actors.Spawning;
using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Modifiers;
using MiniDungeon.Loot.Items.Weapons;
using MiniDungeon.Loot.Spawning;
using MiniDungeon.World.Generation;
using MiniDungeon.World.Generation.Templates;

namespace MiniDungeon.World.Themes;

public class LibraryTheme : IDungeonTheme
{
    public string EntryMessage => "The smell of old books fills the air...";

    public IGenerationTemplate GenerationTemplate => new LibraryTemplate();

    public ILootProvider LootProvider { get; } = new LibraryLootFactory();
    public IEnemyProvider EnemyProvider { get; } = new LibraryEnemyFactory();
    
    public IItem CreateArtifact() => new IntelligenceModifier(
        new MagicWeapon("Dark Wand", 6),
        "",
        10);
}