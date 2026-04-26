using MiniDungeon.Actors.Enemies;

namespace MiniDungeon.Actors.Spawning;

public interface IEnemyProvider
{
    Func<EnemyEntity> GetRandomRecipe();
    IEntity SpawnRandomEntity();
}