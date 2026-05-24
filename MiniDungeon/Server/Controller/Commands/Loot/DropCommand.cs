using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Shared.DTOs.Commands;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class DropCommand(DropArgs args) : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        var session = context.Session;
        var player = context.Player;
        
        var inv = player.Inventory;
        var slot = args.InventorySlot;
        var cell = session.Board[player.Position];

        if (inv.Count <= 0 || slot < 0 || slot >= inv.Count)
        {
            Journal.Instance.Log("No item in selected slot.", player.Id, context.PlayerLogs);
            return false;
        }
        inv.SelectedSlot = slot;
        
        if (inv.TryRemove(slot, out var item))
        {
            if (!cell.TryAddItem(item!))
            {
                inv.TryAdd(item!, slot);
                Journal.Instance.Log($"Failed to drop the {item!.Name}.", player.Id, context.PlayerLogs);
                return false;
            }
            
            Journal.Instance.Log($"You dropped the {item!.Name}.", player.Id, context.PlayerLogs);
            return true;
        }
        
        Journal.Instance.Log("Failed to drop the item.", player.Id, context.PlayerLogs);
        return false;
    }
}