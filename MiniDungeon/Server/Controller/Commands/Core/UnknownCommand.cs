using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server.Controller.Commands.Core;

public class UnknownCommand : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        Journal.Instance.Log("Unknown command.", context.Player.Id, context.PlayerLogs);
        return false;
    }
}