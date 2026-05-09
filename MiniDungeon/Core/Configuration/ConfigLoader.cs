using System.Text.Json;

namespace MiniDungeon.Core.Configuration;

public static class ConfigLoader
{
    public static Config Load(string path)
    {
        if (!File.Exists(path)) return new Config();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Config>(json,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
            ?? new Config();
    }
}