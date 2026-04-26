using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Combat.Attacks;
using MiniDungeon.World.Systems;

namespace MiniDungeon.Loot.Items.Weapons;

public abstract class WeaponItem(string name, int damage, bool isTwoHanded = false) 
    : InventoryItem(name), IWeaponItem
{
    public virtual int Damage { get; } = damage;
    public bool IsTwoHanded { get; } = isTwoHanded;
    
    public INoiseSubject? NoiseSubject { get; set; }

    public override bool Equip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand)
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
        
        IInventoryItem? heldItem = null;
        if (equipment[slot] != null)
        {
            equipment.TryUnequip(slot, out heldItem);
        }

        inventory.TryRemove(item);
        
        if (IsTwoHanded)
        {
            IInventoryItem? otherHeldItem = null;
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

    public abstract CombatStats Accept(IAttackVisitor visitor, Player player, IWeaponItem weapon);
    public override CombatStats Accept(IAttackVisitor visitor, Player player)
        => Accept(visitor, player, this);
}