namespace MiniDungeon.Actors.Spawning;

public class WorkshopEnemyFactory : EnemyFactory
{
    public WorkshopEnemyFactory()
    {
        Enemies.Add(() => new EnemyEntity("Drone", 18, 8, 2));
        Enemies.Add(() => new EnemyEntity("Robot", 35, 9, 1));
        Enemies.Add(() => new EnemyEntity("Inventor", 16, 7, 4));
    }
}