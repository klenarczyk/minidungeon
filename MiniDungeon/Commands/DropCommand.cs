using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class DropCommand : ICommand
{
    public void Execute(GameSession session)
    {
        var player = session.Player;
        var inv = player.Inventory;
        var cell = session.Board[player.Position];

        if (inv.Count <= 0 || inv.SelectedSlot < 0 || inv.SelectedSlot >= inv.Count) return;

        var selectedSlot = inv.SelectedSlot;
        if (inv.TryRemoveAt(selectedSlot, out var item))
        {
            if (!cell.TryAddItem(item!))
            {
                inv.TryAddAt(item!, selectedSlot);
                session.Message = $"Failed to drop {item!.Name}.";
                return;
            }
            
            session.Message = $"You dropped {item!.Name}.";
            return;
        }
        
        session.Message = $"Failed to drop item.";
    }
}