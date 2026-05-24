namespace MiniDungeon.Shared.Logging;

public interface ILogger
{
    void Log(string message, int? playerId = null, List<string>? playerLogs = null);
    IReadOnlyList<string> Entries { get; }
    void OnExit();
}