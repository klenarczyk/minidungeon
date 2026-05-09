namespace MiniDungeon.Server.Model.World.Generation.Templates;

public interface IGenerationTemplate
{
    void Use(IDungeonBuilder builder);
}