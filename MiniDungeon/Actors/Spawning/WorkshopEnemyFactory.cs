using MiniDungeon.Actors.Enemies;
using MiniDungeon.Actors.Enemies.Behaviors;

namespace MiniDungeon.Actors.Spawning;

public class WorkshopEnemyFactory : EnemyFactory
{
    public WorkshopEnemyFactory()
    {
        AddSpecies("Drone", 18, 8, 2, new CowardlyBehavior());
        AddSpecies("Robot", 35, 9, 1, new VengefulBehavior());
        AddSpecies("Inventor", 16, 7, 4, new VengefulBehavior());
    }
}