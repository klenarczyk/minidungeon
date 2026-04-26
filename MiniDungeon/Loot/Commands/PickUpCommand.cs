using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Logging;

namespace MiniDungeon.Loot.Commands;

public class PickUpCommand : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
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