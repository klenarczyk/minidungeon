using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Components;

public enum EquipmentSlot
{
    LeftHand = 0,
    RightHand = 1
}

public class Equipment
{
    private readonly Dictionary<EquipmentSlot, IInventoryItem?> _slots = new();

    public Equipment()
    {
        foreach (var slot in Enum.GetValues<EquipmentSlot>())
        {
            _slots[slot] = null;
        }
    }

    public IInventoryItem? this[EquipmentSlot slot] => _slots[slot];
    
    public bool TryEquip(IInventoryItem item, EquipmentSlot slot, bool isTwoHanded = false)
    {
        if (_slots[slot] != null) return false;

        if (isTwoHanded)
        {
            var otherHand = slot == EquipmentSlot.LeftHand
                ? EquipmentSlot.RightHand
                : EquipmentSlot.LeftHand;
            
            if (_slots[otherHand] != null) return false; // Other hand must be free

            _slots[otherHand] = item;
        }
        
        _slots[slot] = item;
        return true;
    }
    
    public bool TryUnequip(EquipmentSlot slot, out IInventoryItem? item)
    {
        item = null;
        if (_slots[slot] == null) return false;
        
        var otherHand = slot == EquipmentSlot.LeftHand
            ? EquipmentSlot.RightHand
            : EquipmentSlot.LeftHand;

        item = _slots[slot];
        _slots[slot] = null;
        
        if (item == _slots[otherHand]) // Two-handed item equipped
        {
            _slots[otherHand] = null;
        }

        return true;
    }
    
    // Attributes
    public int GetHealthBonus() => GetTotalStatBonus(item => item.GetHealthBonus());
    public int GetStrengthBonus() => GetTotalStatBonus(item => item.GetStrengthBonus());
    public int GetDefenseBonus() => GetTotalStatBonus(item => item.GetDefenseBonus());
    public int GetIntelligenceBonus() => GetTotalStatBonus(item => item.GetIntelligenceBonus());
    public int GetDexterityBonus() => GetTotalStatBonus(item => item.GetDexterityBonus());
    public int GetLuckBonus() => GetTotalStatBonus(item => item.GetLuckBonus());
    
    // Helpers
    private int GetTotalStatBonus(Func<IInventoryItem, int> getStatBonus)
    {
        var bonus = 0;
        
        var itemL = _slots[EquipmentSlot.LeftHand];
        var itemR = _slots[EquipmentSlot.RightHand];
        
        if (itemL != null) bonus += getStatBonus(itemL);
        if (itemR != null && itemL != itemR) bonus += getStatBonus(itemR);

        return bonus;
    }
}