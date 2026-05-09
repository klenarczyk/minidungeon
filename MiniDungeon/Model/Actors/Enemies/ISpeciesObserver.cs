namespace MiniDungeon.Model.Actors.Enemies;

public interface ISpeciesObserver
{
    void Update(ISpeciesSubject species);
}