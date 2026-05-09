namespace MiniDungeon.Server.Model.Actors.Enemies;

public interface ISpeciesSubject
{
    void Attach(ISpeciesObserver observer);
    void Detach(ISpeciesObserver observer);
    void Notify();
}