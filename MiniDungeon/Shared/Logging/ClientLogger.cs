namespace MiniDungeon.Shared.Logging;

public class ClientLogger : ILogger
{
    private readonly List<string> _entries = [];
    public IReadOnlyList<string> Entries => _entries;
    
    public void Log(string message, int? playerId = null, List<string>? playerLogs = null)
    {
        _entries.Add(message);
    }

    public void OnExit() { }
}