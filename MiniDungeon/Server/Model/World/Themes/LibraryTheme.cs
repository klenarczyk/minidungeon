using MiniDungeon.Server.Model.Actors.Spawning;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Modifiers;
using MiniDungeon.Server.Model.Loot.Items.Weapons;
using MiniDungeon.Server.Model.Loot.Spawning;
using MiniDungeon.Server.Model.World.Generation.Templates;

namespace MiniDungeon.Server.Model.World.Themes;

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