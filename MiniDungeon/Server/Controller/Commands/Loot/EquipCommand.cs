using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Logging;
using MiniDungeon.Server.Model.Loot;

namespace MiniDungeon.Server.Controller.Commands.Loot;

public class EquipCommand(EquipmentSlot eqSlot, int invSlot) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var player = context.Player;
        
        var inventory = player.Inventory;
        var equipment = player.Equipment;
        
        inventory.SelectedSlot = invSlot;
        var item = inventory.SelectedItem;

        if (item == null)
        {
            if (equipment[eqSlot] == null)
            {
                context.PopInputChain();
                return false;
            }

            if (!inventory.HasSpace)
            {
                Journal.Instance.Log("Inventory full!", player.Id, context.PlayerLogs);
                context.PopInputChain();
                return false;
            }

            equipment.TryUnequip(eqSlot, out item);
            inventory.TryAdd(item!);
            
            Journal.Instance.Log($"You unequipped the {item!.Name}.", player.Id, context.PlayerLogs);
            context.PopInputChain();
            return true;
        };

        var message = item.Equip(player, eqSlot) 
            ? $"You equipped the {item.Name}." 
            : $"Cannot equip the two-handed {item.Name}.";
        Journal.Instance.Log(message, player.Id, context.PlayerLogs);
        
        context.PopInputChain();
        return false;
    }
}