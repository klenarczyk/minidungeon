namespace MiniDungeon.Actors.Spawning;

public class TreasuryEnemyFactory : EnemyFactory
{
    public TreasuryEnemyFactory()
    {
        Enemies.Add(() => new EnemyEntity("Goblin", 18, 8, 2));
        Enemies.Add(() => new EnemyEntity("Guard", 35, 9, 1));
        Enemies.Add(() => new EnemyEntity("Mimic", 16, 7, 4));
    }
}