using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class DropCommand(int invSlot) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        
        if (invSlot == -1) // Cancellation
        {
            session.Message = "";
            context.PopInputChain();
            return;
        }
        
        var player = session.Player;
        var inv = player.Inventory;
        var cell = session.Board[player.Position];

        if (inv.Count <= 0 || invSlot < 0 || invSlot >= inv.Count)
        {
            session.Message = "No item in selected slot.";
            context.PopInputChain();
            return;
        }
        inv.SelectedSlot =  invSlot;
        
        if (inv.TryRemove(invSlot, out var item))
        {
            if (!cell.TryAddItem(item!))
            {
                inv.TryAdd(item!, invSlot);
                session.Message = $"Failed to drop the {item!.GetName()}.";
                context.PopInputChain();
                return;
            }
            
            session.Message = $"You dropped the {item!.GetName()}.";
            context.PopInputChain();
            return;
        }
        
        session.Message = "Failed to drop the item.";
        
        context.PopInputChain();
    }
}