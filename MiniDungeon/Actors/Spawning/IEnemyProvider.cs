namespace MiniDungeon.Actors.Spawning;

public interface IEnemyProvider
{
    IEntity SpawnRandomEntity();
}