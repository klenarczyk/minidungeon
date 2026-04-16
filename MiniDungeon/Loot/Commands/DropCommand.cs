using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Logging;

namespace MiniDungeon.Loot.Commands;

public class DropCommand(int invSlot) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        
        var player = session.Player;
        var inv = player.Inventory;
        var cell = session.Board[player.Position];

        if (inv.Count <= 0 || invSlot < 0 || invSlot >= inv.Count)
        {
            Journal.Instance.Log("No item in selected slot.");
            context.PopInputChain();
            return;
        }
        inv.SelectedSlot =  invSlot;
        
        if (inv.TryRemove(invSlot, out var item))
        {
            if (!cell.TryAddItem(item!))
            {
                inv.TryAdd(item!, invSlot);
                Journal.Instance.Log($"Failed to drop the {item!.Name}.");
                context.PopInputChain();
                return;
            }
            
            Journal.Instance.Log($"You dropped the {item!.Name}.");
            context.PopInputChain();
            return;
        }
        
        Journal.Instance.Log("Failed to drop the item.");
        
        context.PopInputChain();
    }
}