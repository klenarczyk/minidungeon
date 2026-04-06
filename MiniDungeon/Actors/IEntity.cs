namespace MiniDungeon.Actors;

public interface IEntity
{
    string Name { get; }
    int Health { get; }
    bool IsDead { get; }
    void TakeDamage(int damage);
    int DealDamage();
}