using MiniDungeon.Client;
using MiniDungeon.Server;

if (args.Length == 0)
{
    PrintUsage();
    return;
}

var mode = args[0].ToLower();

switch (mode)
{
    case "--server":
    {
        var port = args.Length > 1 && int.TryParse(args[1], out var portNum) 
            ? portNum 
            : 5555;
    
        Console.Title = $"MiniDungeon Server (Port: {port})";

        var server = new GameServer(port);
        await server.Run();
        break;
    }
    case "--client":
    {
        var ip = "127.0.0.1";
        var port = 5555;

        if (args.Length > 1)
        {
            var addr = args[1].Split(':');
            ip = addr[0];

            if (addr.Length > 1 && int.TryParse(addr[1], out var portNum))
            {
                port = portNum;
            }
        }
    
        Console.Title = $"MiniDungeon ({ip}:{port})";
    
        var client = new GameClient(ip, port);
        await client.Run();
        break;
    }
    default:
        PrintUsage();
        break;
}

return;

void PrintUsage()
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  MiniDungeon.exe --server [port]           (Default: 5555)");
    Console.WriteLine("  MiniDungeon.exe --client [ip:port]        (Default: 127.0.0.1:5555)");
}