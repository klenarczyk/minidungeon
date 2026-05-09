namespace MiniDungeon.Server.Model.Actors.Enemies;

public interface ISpeciesObserver
{
    void Update(ISpeciesSubject species);
}