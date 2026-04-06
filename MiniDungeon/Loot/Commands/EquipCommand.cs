using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;

namespace MiniDungeon.Loot.Commands;

public class EquipCommand(EquipmentSlot eqSlot, int invSlot) : ICommand
{
    public void Execute(IGameContext context)
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
                session.Message = "";
                context.PopInputChain();
                return;
            }

            if (!inventory.HasSpace)
            {
                session.Message = "Inventory full!";
                context.PopInputChain();
                return;
            }

            equipment.TryUnequip(eqSlot, out item);
            inventory.TryAdd(item!);
            
            session.Message = $"You unequipped the {item!.Name}.";
            context.PopInputChain();
            return;
        };

        session.Message = item.Equip(player, eqSlot) 
            ? $"You equipped the {item.Name}." 
            : $"Cannot equip the two-handed {item.Name}.";
        
        context.PopInputChain();
    }
}