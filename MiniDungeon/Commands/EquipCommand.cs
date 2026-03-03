using MiniDungeon.Components;
using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class EquipCommand(EquipmentSlot slot) : ICommand
{
    public void Execute(GameSession session)
    {
        var player = session.Player;
        var inventory = session.Player.Inventory;
        var equipment = session.Player.Equipment;
        var item = inventory.SelectedItem;

        if (item == null)
        {
            if (equipment[slot] == null) return;

            if (!inventory.HasSpace)
            {
                session.Message = "Inventory full!";
                return;
            }

            equipment.TryUnequip(slot, out item);
            inventory.TryAdd(item!);
            
            session.Message = $"You unequipped the {item!.Name}.";
            return;
        };

        session.Message = item.OnEquip(player, slot) 
            ? $"You equipped the {item.Name}." 
            : $"Cannot equip the two-handed {item.Name}.";
    }
}