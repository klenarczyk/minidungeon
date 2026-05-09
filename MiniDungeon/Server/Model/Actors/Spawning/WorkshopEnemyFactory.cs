using MiniDungeon.Server.Model.Actors.Enemies.Behaviors;

namespace MiniDungeon.Server.Model.Actors.Spawning;

public class WorkshopEnemyFactory : EnemyFactory
{
    public WorkshopEnemyFactory()
    {
        AddSpecies("Drone", 18, 8, 2, new CowardlyBehavior());
        AddSpecies("Robot", 35, 9, 1, new VengefulBehavior());
        AddSpecies("Inventor", 16, 7, 4, new VengefulBehavior());
    }
}