using MiniDungeon.Model.Actors.Spawning;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Modifiers;
using MiniDungeon.Model.Loot.Items.Weapons;
using MiniDungeon.Model.Loot.Spawning;
using MiniDungeon.Model.World.Generation.Templates;

namespace MiniDungeon.Model.World.Themes;

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