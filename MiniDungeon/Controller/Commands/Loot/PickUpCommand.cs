using MiniDungeon.Controller.Commands.Core;
using MiniDungeon.Core.Logging;

namespace MiniDungeon.Controller.Commands.Loot;

public class PickUpCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Players[0];
        var cell = session.Board[player.Position];

        if (cell.Items.Count == 0) return false;

        var item = cell.Items[0];
        if (!item.Collect(player, cell))
        {
            session.Message = "Inventory full!";
            return false;
        } 
        
        Journal.Instance.Log($"You picked up the {item.Name}.");
        return true;
    }
}