using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class PickUpCommand : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        var session = context.Session;
        var player = context.Player;
        var cell = session.Board[player.Position];

        if (cell.Items.Count == 0) return false;

        var item = cell.Items[0];
        if (!item.Collect(player, cell))
        {
            return false;
        } 
        
        Journal.Instance.Log($"Picked up the {item.Name}.", player.Id, context.PlayerLogs);
        return true;
    }
}