using MiniDungeon.Components;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items;

public class InventoryItem(string name) : IItem
{
    public string GetName() => name;
    
    public bool OnPickup(Player player, Cell cell)
    {
        if (!cell.TryRemoveItem(this)) return false;
        if (!player.Inventory.TryAdd(this))
        {
            cell.TryAddItem(this);
            return false;
        }
        
        return true;
    }
    
    public virtual bool OnEquip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand) => false;
}