using MiniDungeon.Model.Actors.Enemies.Behaviors;

namespace MiniDungeon.Model.Actors.Spawning;

public class TreasuryEnemyFactory : EnemyFactory
{
    public TreasuryEnemyFactory()
    {
        AddSpecies("Goblin", 18, 8, 2, new CowardlyBehavior());
        AddSpecies("Guard", 35, 9, 1, new VengefulBehavior());
        AddSpecies("Mimic", 16, 7, 4, new CowardlyBehavior());
    }
}