using MiniDungeon.Actors.Enemies;
using MiniDungeon.Actors.Enemies.Behaviors;

namespace MiniDungeon.Actors.Spawning;

public abstract class EnemyFactory : IEnemyProvider
{
    private readonly List<Func<EnemyEntity>> _enemies = [];
    private readonly Random _random = new();

    public Func<EnemyEntity> GetRandomRecipe() => _enemies[_random.Next(_enemies.Count)];
    
    public IEntity SpawnRandomEntity()
    {
        var recipe = _enemies[_random.Next(_enemies.Count)];
        return recipe.Invoke();
    }
    
    protected void AddSpecies(string name, int health, int attack, int armor, ISpeciesBehavior behavior)
    {
        var speciesSubject = new SpeciesNotifier();
        _enemies.Add(() =>
        {
            var species = new EnemyEntity(name, health, attack, armor, behavior, speciesSubject);
            speciesSubject.Attach(species);
            return species;
        });
    }
}