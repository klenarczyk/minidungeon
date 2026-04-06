namespace MiniDungeon.Actors.Spawning;

public class EnemyFactory : IEnemyProvider
{
    private readonly List<Func<EnemyEntity>> _enemies = [];
    private readonly Random _random = new();

    public EnemyFactory()
    {
        _enemies.Add(() => new EnemyEntity("Goblin", 24, 8, 2));
        _enemies.Add(() => new EnemyEntity("Orc", 35, 9, 1));
        _enemies.Add(() => new EnemyEntity("Skeleton", 16, 7, 4));
    }
    
    public IEntity SpawnRandomEntity()
    {
        var recipe = _enemies[_random.Next(_enemies.Count)];
        return recipe.Invoke();
    }
}