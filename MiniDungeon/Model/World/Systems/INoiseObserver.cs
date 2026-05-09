namespace MiniDungeon.Model.World.Systems;

public interface INoiseObserver
{
    void Update(INoiseSubject noise);
}