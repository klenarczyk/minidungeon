namespace MiniDungeon.Server.Model.Actors.Enemies;

public class SpeciesNotifier : ISpeciesSubject
{
    private readonly List<ISpeciesObserver> _observers = [];
    
    public void Attach(ISpeciesObserver observer)
    {
        if (_observers.Contains(observer)) return;
        _observers.Add(observer);
    }

    public void Detach(ISpeciesObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}