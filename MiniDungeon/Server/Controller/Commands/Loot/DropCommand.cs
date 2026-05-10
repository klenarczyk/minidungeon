using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Logging;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class DropCommand(int invSlot) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = context.Player;
        
        var inv = player.Inventory;
        var cell = session.Board[player.Position];

        if (inv.Count <= 0 || invSlot < 0 || invSlot >= inv.Count)
        {
            Journal.Instance.Log("No item in selected slot.", player.Id, context.PlayerLogs);
            context.PopInputChain();
            return false;
        }
        inv.SelectedSlot =  invSlot;
        
        if (inv.TryRemove(invSlot, out var item))
        {
            if (!cell.TryAddItem(item!))
            {
                inv.TryAdd(item!, invSlot);
                Journal.Instance.Log($"Failed to drop the {item!.Name}.", player.Id, context.PlayerLogs);
                context.PopInputChain();
                return false;
            }
            
            Journal.Instance.Log($"You dropped the {item!.Name}.", player.Id, context.PlayerLogs);
            context.PopInputChain();
            return true;
        }
        
        Journal.Instance.Log("Failed to drop the item.", player.Id, context.PlayerLogs);
        
        context.PopInputChain();
        return false;
    }
}