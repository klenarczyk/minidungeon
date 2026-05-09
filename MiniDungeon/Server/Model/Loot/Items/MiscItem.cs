using MiniDungeon.Server.Model.Actors;

namespace MiniDungeon.Server.Model.Loot.Items;

public class MiscItem(string name) : InventoryItem(name)
{
    public override bool Equip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand)
    {
        var inventory = player.Inventory;
        var equipment = player.Equipment;
        var item = inventory.SelectedItem;
        if (item == null) return false;

        IInventoryItem? heldItem = null;
        if (equipment[slot] != null)
        {
            equipment.TryUnequip(slot, out heldItem);
        }
        
        inventory.TryRemove(item);
        equipment.TryEquip(item, slot);

        if (heldItem != null)
        {
            inventory.TryAdd(heldItem);
        }

        return true;
    }
}