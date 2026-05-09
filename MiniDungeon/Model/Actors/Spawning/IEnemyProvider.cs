using MiniDungeon.Model.Actors.Enemies;
using MiniDungeon.Model.World.Systems;

namespace MiniDungeon.Model.Actors.Spawning;

public interface IEnemyProvider
{
    INoiseSubject? NoiseSubject { get; set; }
    
    Func<EnemyEntity> GetRandomRecipe();
    IEntity SpawnRandomEntity();
}