using MiniDungeon.Actors.Enemies;
using MiniDungeon.World.Systems;

namespace MiniDungeon.Actors.Spawning;

public interface IEnemyProvider
{
    INoiseSubject? NoiseSubject { get; set; }
    
    Func<EnemyEntity> GetRandomRecipe();
    IEntity SpawnRandomEntity();
}