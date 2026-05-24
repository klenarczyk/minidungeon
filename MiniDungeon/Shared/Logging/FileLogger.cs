using MiniDungeon.Shared.Configuration;

namespace MiniDungeon.Shared.Logging;

public class FileLogger : ILogger
{
    private readonly List<string> _entries = [];
    
    private readonly string _filePath;
    private readonly Lock _lock = new();

    public IReadOnlyList<string> Entries
    {
        get
        {
            lock (_lock)
            {
                return _entries.ToList();
            }
        }
    }

    public FileLogger(Config config)
    {
        Directory.CreateDirectory(config.JournalPath);
        _filePath = Path.Combine(config.JournalPath, 
            $"{config.PlayerName}-[{DateTime.Now:yyyy-MM-dd_HH-mm}].txt");
    }
    
    public void Log(string message, int? playerId = null, List<string>? playerLogs = null)
    {
        lock (_lock)
        {
            playerLogs?.Add(message);
            
            if (playerId != null) message = $"[Player {playerId}] {message}";
           
            _entries.Add(message);
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }

    public void OnExit()
    {
        Console.WriteLine("Log file: " + _filePath);
    }
}