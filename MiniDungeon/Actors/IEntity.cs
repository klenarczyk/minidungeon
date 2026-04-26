using MiniDungeon.Actors.Enemies;
using MiniDungeon.Engine;
using MiniDungeon.World;
using MiniDungeon.World.Systems;

namespace MiniDungeon.Actors;

public interface IEntity : ISpeciesObserver, INoiseObserver
{
    string Name { get; }
    int Health { get; }
    bool IsDead { get; }
    int AttackDmg { get; set; }
    int Armor { get; set; }
    Position Position { get; set; }
    int TakeDamage(int damage);
    int DealDamage();
    void Die(GameSession session);
    void Move(IGameContext context);
}