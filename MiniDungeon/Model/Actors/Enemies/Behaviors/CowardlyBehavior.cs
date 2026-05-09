namespace MiniDungeon.Model.Actors.Enemies.Behaviors;

public class CowardlyBehavior : ISpeciesBehavior
{
    public void Act(IEntity entity)
    {
        entity.Armor /= 2;
    }
}