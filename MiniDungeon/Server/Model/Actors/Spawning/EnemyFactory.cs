using MiniDungeon.Server.Model.Actors.Enemies;
using MiniDungeon.Server.Model.Actors.Enemies.Behaviors;
using MiniDungeon.Server.Model.World.Systems;

namespace MiniDungeon.Server.Model.Actors.Spawning;

public abstract class EnemyFactory : IEnemyProvider
{
    public INoiseSubject? NoiseSubject { get; set; }
    
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
            var enemy = new EnemyEntity(name, health, attack, armor, behavior, speciesSubject, NoiseSubject);
            speciesSubject.Attach(enemy);
            NoiseSubject?.Attach(enemy);
            return enemy;
        });
    }
}