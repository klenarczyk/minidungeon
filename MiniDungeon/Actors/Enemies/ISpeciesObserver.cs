namespace MiniDungeon.Actors.Enemies;

public interface ISpeciesObserver
{
    void Update(ISpeciesSubject species);
}