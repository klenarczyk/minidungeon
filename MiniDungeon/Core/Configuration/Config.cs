using System.Text.Json;

namespace MiniDungeon.Core.Configuration;

public class Config
{
    public string PlayerName { get; set; } = "Player";
    public string JournalPath { get; set; } = "logs/";
    
    public static Config Load(string path)
    {
        if (!File.Exists(path)) return new Config();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Config>(json,
                   new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
               ?? new Config();
    }
}