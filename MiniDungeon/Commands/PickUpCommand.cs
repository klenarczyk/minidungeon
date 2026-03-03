using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class PickUpCommand : ICommand
{
    public void Execute(GameSession session)
    {
        var player = session.Player;
        var cell = session.Board[player.Position];

        if (cell.Items.Count == 0) return;

        var item = cell.Items[0];
        if (!item.OnPickup(player, cell))
        {
            session.Message = "Inventory full!";
            return;
        } 
        
        session.Message = $"You picked up the {item.Name}.";
    }
}