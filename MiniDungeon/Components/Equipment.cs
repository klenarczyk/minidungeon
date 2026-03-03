using MiniDungeon.Items;

namespace MiniDungeon.Components;

public enum EquipmentSlot
{
    LeftHand = 0,
    RightHand = 1
}

public class Equipment
{
    private readonly Dictionary<EquipmentSlot, InventoryItem?> _slots = new();

    public Equipment()
    {
        foreach (var slot in Enum.GetValues<EquipmentSlot>())
        {
            _slots[slot] = null;
        }
    }

    public InventoryItem? this[EquipmentSlot slot] => _slots[slot];
    
    public bool TryEquip(InventoryItem item, EquipmentSlot slot, bool isTwoHanded = false)
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
    
    public bool TryUnequip(EquipmentSlot slot, out InventoryItem? item)
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
}