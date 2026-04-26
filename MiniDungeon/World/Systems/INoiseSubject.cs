namespace MiniDungeon.World.Systems;

public interface INoiseSubject
{
    void Attach(INoiseObserver observer);
    void Detach(INoiseObserver observer);
    void Notify(Position origin, int radius);
}