namespace MiniDungeon.Actors;

public interface IEntity
{
    string Name { get; }
    int Health { get; }
    bool IsDead { get; }
    int TakeDamage(int damage);
    int DealDamage();
}