using MiniDungeon.Components;
using MiniDungeon.Entities;

namespace MiniDungeon.Items;

public abstract class WeaponItem(string name, int damage, bool isTwoHanded = false) : InventoryItem(name)
{
    public int Damage { get; } = damage;
    public bool IsTwoHanded { get; } = isTwoHanded;

    public override bool OnEquip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand)
    {
        var inventory = player.Inventory;
        var equipment = player.Equipment;
        var item = inventory.SelectedItem;
        if (item == null) return false;

        var otherHand = slot == EquipmentSlot.LeftHand
            ? EquipmentSlot.RightHand
            : EquipmentSlot.LeftHand;
        
        if (IsTwoHanded 
            && equipment[slot] != null 
            && equipment[otherHand] != null
            && equipment[slot] != equipment[otherHand]
            && inventory.Count == inventory.Capacity) return false;
        
        InventoryItem? heldItem = null;
        if (equipment[slot] != null)
        {
            equipment.TryUnequip(slot, out heldItem);
        }

        inventory.TryRemove(item);
        
        if (IsTwoHanded)
        {
            InventoryItem? otherHeldItem = null;
            if (equipment[otherHand] != null)
            {
                equipment.TryUnequip(otherHand, out otherHeldItem);
            }

            if (otherHeldItem != null)
            {
                inventory.TryAdd(otherHeldItem);
            }
        }
        
        equipment.TryEquip(item, slot);
        if (IsTwoHanded) equipment.TryEquip(item, otherHand);

        if (heldItem != null)
        {
            inventory.TryAdd(heldItem);
        }

        return true;
    }
}