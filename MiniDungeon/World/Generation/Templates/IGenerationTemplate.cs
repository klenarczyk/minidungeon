namespace MiniDungeon.World.Generation.Templates;

public interface IGenerationTemplate
{
    void Use(IDungeonBuilder builder);
}