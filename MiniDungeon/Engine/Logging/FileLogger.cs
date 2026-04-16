using MiniDungeon.Engine.Configuration;

namespace MiniDungeon.Engine.Logging;

public class FileLogger : ILogger
{
    private readonly List<string> _entries = [];
    public IReadOnlyList<string> Entries => _entries;
    
    private readonly string _filePath;

    public FileLogger(Config config)
    {
        Directory.CreateDirectory(config.JournalPath);
        _filePath = Path.Combine(config.JournalPath, 
            $"{config.PlayerName}-[{DateTime.Now:yyyy-MM-dd_HH-mm}].txt");
    }
    
    public void Log(string message)
    {
        _entries.Add(message);
        File.AppendAllText(_filePath, message + Environment.NewLine);
    }

    public void OnExit()
    {
        Console.WriteLine("Log file: " + _filePath);
    }
}