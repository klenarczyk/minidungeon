namespace MiniDungeon.Server.Model.Actors.Enemies.Behaviors;

public class VengefulBehavior : ISpeciesBehavior
{
    public void Act(IEntity entity)
    {
        entity.AttackDmg = (int)Math.Ceiling(entity.AttackDmg * 1.5);
    }
}