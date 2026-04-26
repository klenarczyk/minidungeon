namespace MiniDungeon.Actors;

public interface IEntity
{
    string Name { get; }
    int Health { get; }
    bool IsDead { get; }
    int AttackDmg { get; set; }
    int Armor { get; set; }
    int TakeDamage(int damage);
    int DealDamage();
    void Die();
}