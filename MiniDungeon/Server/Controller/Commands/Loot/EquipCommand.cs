using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Shared.DTOs.Commands;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class EquipCommand(EquipArgs args) : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        var player = context.Player;
        
        var inventory = player.Inventory;
        var invSlot = args.InventorySlot;
        
        var equipment = player.Equipment;
        var eqSlot = args.EquipmentSlot;
        
        inventory.SelectedSlot = invSlot;
        var item = inventory.SelectedItem;

        if (item == null)
        {
            if (equipment[eqSlot] == null) return false;

            if (!inventory.HasSpace)
            {
                Journal.Instance.Log("Inventory full!", player.Id, context.PlayerLogs);
                return false;
            }

            equipment.TryUnequip(eqSlot, out item);
            inventory.TryAdd(item!);
            
            Journal.Instance.Log($"You unequipped the {item!.Name}.", player.Id, context.PlayerLogs);
            return true;
        };

        var message = item.Equip(player, eqSlot) 
            ? $"You equipped the {item.Name}." 
            : $"Cannot equip the two-handed {item.Name}.";
        Journal.Instance.Log(message, player.Id, context.PlayerLogs);
        
        return false;
    }
}