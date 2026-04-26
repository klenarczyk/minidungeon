using MiniDungeon.Actors.Enemies.Behaviors;

namespace MiniDungeon.Actors.Enemies;

public class EnemyEntity(
    string name, 
    int health, 
    int attack, 
    int armor, 
    ISpeciesBehavior speciesBehavior,
    ISpeciesSubject speciesNotifier
    ) : IEntity, ISpeciesObserver
{
    public string Name { get; } = name;
    
    public int Health { get; private set; } = health;
    public bool IsDead => Health <= 0;

    public int AttackDmg { get; set; } = attack;
    public int Armor { get; set; } = armor;

    public int TakeDamage(int damage)
    {
        var realDmg = Math.Max(0, damage - Armor);
        Health -= realDmg;
        return realDmg;
    }

    public int DealDamage()
    {
        var random = new Random();

        if (random.Next(10) == 0) // Critical hit
        {
            return (int)(AttackDmg * 1.5);
        }

        return AttackDmg;
    }

    public void Update(ISpeciesSubject species) => speciesBehavior.Act(this);

    public void Die()
    {
        speciesNotifier.Notify();
        speciesNotifier.Detach(this);
    }
}