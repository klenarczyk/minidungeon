namespace MiniDungeon.Core.Logging;

public interface ILogger
{
    void Log(string message);
    IReadOnlyList<string> Entries { get; }
    void OnExit();
}