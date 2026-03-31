using MiniDungeon.Entities;

namespace MiniDungeon.Providers;

public interface IEnemyProvider
{
    IEntity SpawnRandomEntity();
}