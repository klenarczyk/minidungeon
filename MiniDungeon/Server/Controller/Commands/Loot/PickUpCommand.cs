using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Logging;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class PickUpCommand : ICommand
{
    public bool Execute(IGameContext context)
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
        
        Journal.Instance.Log($"You picked up the {item.Name}.", player.Id, context.PlayerLogs);
        return true;
    }
}