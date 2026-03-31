using MiniDungeon.Components;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Items;

public class InventoryItem(string name) : IItem
{
    public virtual string Name { get; } = name;
    
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

    public virtual int GetHealthBonus() => 0;
    public virtual int GetStrengthBonus() => 0;
    public virtual int GetDefenseBonus() => 0;
    public virtual int GetIntelligenceBonus() => 0;
    public virtual int GetAggressionBonus() => 0;
    public virtual int GetLuckBonus() => 0;
}