using MiniDungeon.Server.Model.Actors.Enemies;
using MiniDungeon.Server.Model.World.Systems;

namespace MiniDungeon.Server.Model.Actors.Spawning;

public interface IEnemyProvider
{
    INoiseSubject? NoiseSubject { get; set; }
    
    Func<EnemyEntity> GetRandomRecipe();
    IEntity SpawnRandomEntity();
}