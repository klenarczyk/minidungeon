namespace MiniDungeon.Engine.Persistence;

public interface ILogger
{
    void Log(string message);
    IReadOnlyList<string> Entries { get; }
}