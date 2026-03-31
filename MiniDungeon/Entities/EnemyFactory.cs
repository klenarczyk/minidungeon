using MiniDungeon.Providers;

namespace MiniDungeon.Entities;

public class EnemyFactory : IEnemyProvider
{
    private readonly List<Func<EnemyEntity>> _enemies = [];
    private readonly Random _random = new();

    public EnemyFactory()
    {
        _enemies.Add(() => new EnemyEntity("Goblin", 20, 3, 3));
    }
    
    public IEntity SpawnRandomEntity()
    {
        var recipe = _enemies[_random.Next(_enemies.Count)];
        return recipe.Invoke();
    }
}