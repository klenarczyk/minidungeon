using MiniDungeon.Controller.Commands.Core;
using MiniDungeon.Core.Logging;
using MiniDungeon.Model.Loot;

namespace MiniDungeon.Controller.Commands.Loot;

public class EquipCommand(EquipmentSlot eqSlot, int invSlot) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        
        var player = session.Players[0];
        var inventory = session.Players[0].Inventory;
        var equipment = session.Players[0].Equipment;
        
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
                Journal.Instance.Log("Inventory full!");
                context.PopInputChain();
                return false;
            }

            equipment.TryUnequip(eqSlot, out item);
            inventory.TryAdd(item!);
            
            Journal.Instance.Log($"You unequipped the {item!.Name}.");
            context.PopInputChain();
            return true;
        };

        var message = item.Equip(player, eqSlot) 
            ? $"You equipped the {item.Name}." 
            : $"Cannot equip the two-handed {item.Name}.";
        Journal.Instance.Log(message);
        
        context.PopInputChain();
        return false;
    }
}