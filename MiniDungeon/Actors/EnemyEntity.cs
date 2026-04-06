namespace MiniDungeon.Actors;

public class EnemyEntity(string name, int health, int attack, int armor) : IEntity
{
    public string Name { get; } = name;
    
    public int Health { get; protected set; } = health;
    public bool IsDead => Health <= 0;

    public void TakeDamage(int damage)
    {
        var realDmg = Math.Max(0, damage - armor);
        Health -= realDmg;
    }

    public int DealDamage()
    {
        var random = new Random();

        if (random.Next(10) == 0) // Critical hit
        {
            return (int)(attack * 1.5);
        }

        return attack;
    }
}