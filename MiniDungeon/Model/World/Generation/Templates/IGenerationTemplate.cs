namespace MiniDungeon.Model.World.Generation.Templates;

public interface IGenerationTemplate
{
    void Use(IDungeonBuilder builder);
}