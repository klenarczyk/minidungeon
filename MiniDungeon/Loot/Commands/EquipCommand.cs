using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Logging;

namespace MiniDungeon.Loot.Commands;

public class EquipCommand(EquipmentSlot eqSlot, int invSlot) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        
        var player = session.Player;
        var inventory = session.Player.Inventory;
        var equipment = session.Player.Equipment;
        
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