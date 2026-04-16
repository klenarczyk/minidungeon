namespace MiniDungeon.Engine.Logging;

public static class Journal
{
    private static ILogger? _instance;
    public static ILogger Instance 
        => _instance ?? throw new InvalidOperationException("Journal not initialized.");

    public static void Initialize(ILogger logger)
    {
        if (_instance != null)
            throw new InvalidOperationException("Journal already initialized.");
        
        _instance = logger;
    }
}