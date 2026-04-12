namespace MiniDungeon.Actors.Spawning;

public abstract class EnemyFactory : IEnemyProvider
{
    protected readonly List<Func<EnemyEntity>> Enemies = [];
    private readonly Random _random = new();
    
    public IEntity SpawnRandomEntity()
    {
        var recipe = Enemies[_random.Next(Enemies.Count)];
        return recipe.Invoke();
    }
}