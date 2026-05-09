using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Combat;
using MiniDungeon.Model.Combat.Attacks;
using MiniDungeon.Model.World;

namespace MiniDungeon.Model.Loot.Items;

public class InventoryItem(string name) : IInventoryItem
{
    public virtual string Name { get; } = name;
    
    public virtual bool Collect(Player player, Cell cell, IInventoryItem item)
    {
        if (!player.Inventory.TryAdd(item)) return false;
        if (cell.TryRemoveItem(item)) return true;
        
        player.Inventory.TryRemove(item);
        return false;
    }

    public virtual bool Collect(Player player, Cell cell) => Collect(player, cell, this);
    
    public virtual bool Equip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand) => false;

    public virtual int GetHealthBonus() => 0;
    public virtual int GetStrengthBonus() => 0;
    public virtual int GetDefenseBonus() => 0;
    public virtual int GetIntelligenceBonus() => 0;
    public virtual int GetDexterityBonus() => 0;
    public virtual int GetLuckBonus() => 0;
    
    public virtual CombatStats Accept(IAttackVisitor visitor, Player player) => visitor.Visit(this, player);
}