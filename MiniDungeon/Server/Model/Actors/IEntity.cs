using MiniDungeon.Server.Model.Actors.Enemies;
using MiniDungeon.Server.Model.World;
using MiniDungeon.Server.Model.World.Systems;

namespace MiniDungeon.Server.Model.Actors;

public interface IEntity : ISpeciesObserver, INoiseObserver
{
    string Name { get; }
    int Health { get; }
    bool IsDead { get; }
    int AttackDmg { get; set; }
    int Armor { get; set; }
    Position Position { get; set; }
    int? BattledPlayerId { get; set; }
    int TakeDamage(int damage);
    int DealDamage();
    void Die(GameSession session);
    void Move(IGameContext context);
}