namespace MiniDungeon.World.Systems;

public class NoiseNotifier : INoiseSubject
{
    private readonly List<INoiseObserver> _observers = [];

    private List<Position> _area { get; } = [];
    public IReadOnlyList<Position> Area => _area;
    
    public void Attach(INoiseObserver observer)
    {
        if (_observers.Contains(observer)) return;
        _observers.Add(observer);
    }

    public void Detach(INoiseObserver observer) => _observers.Remove(observer);

    public void Notify(Position origin, int radius)
    {
        SetArea(origin, radius);
        
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    private void SetArea(Position origin, int radius)
    {
        // TODO: Add area calculations
        throw new NotImplementedException();
    }
}