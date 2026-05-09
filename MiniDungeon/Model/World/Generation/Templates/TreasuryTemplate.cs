namespace MiniDungeon.Model.World.Generation.Templates;

public class TreasuryTemplate : IGenerationTemplate
{
    public void Use(IDungeonBuilder builder)
    {
        builder
            .InitializeFilled()
            .AddCentralRoom(20, 10)
            .AddItems(20)
            .AddWeapons();
    }
}