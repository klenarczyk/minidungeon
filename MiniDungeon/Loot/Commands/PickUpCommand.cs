using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Persistence;

namespace MiniDungeon.Loot.Commands;

public class PickUpCommand : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
        var cell = session.Board[player.Position];

        if (cell.Items.Count == 0) return;

        var item = cell.Items[0];
        if (!item.Collect(player, cell))
        {
            session.Message = "Inventory full!";
            return;
        } 
        
        Journal.Instance.Log($"You picked up the {item.Name}.");
    }
}