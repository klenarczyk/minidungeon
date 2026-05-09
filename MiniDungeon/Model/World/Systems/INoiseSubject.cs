namespace MiniDungeon.Model.World.Systems;

public interface INoiseSubject
{
    Position Origin { get; }
    IReadOnlyList<Position> Area { get; }
    
    void Attach(INoiseObserver observer);
    void Detach(INoiseObserver observer);
    void Notify(Position origin, int maxDistance);
}