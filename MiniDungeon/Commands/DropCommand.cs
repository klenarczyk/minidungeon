using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class DropCommand : ICommand
{
    public void Execute(GameSession session)
    {
        var player = session.Player;
        var cell = session.Board[player.Position];

        if (player.Inventory.Count <= 0) return;

        var item = player.Inventory.Items[0];
        if (player.Inventory.TryRemove(item))
        {
            if (!cell.TryAddItem(item))
                session.Message = $"Failed to drop {item.Name}.";

            session.Message = $"You dropped {item.Name}.";

            return;
        }
        
        session.Message = $"Failed to drop {item.Name}.";
    }
}