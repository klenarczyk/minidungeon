using MiniDungeon.Combat;
using MiniDungeon.Components;
using MiniDungeon.Entities;

namespace MiniDungeon.Items.Abstractions;

public interface IInventoryItem : IItem
{
    bool OnEquip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand);

    int GetHealthBonus();
    int GetStrengthBonus();
    int GetDefenseBonus();
    int GetIntelligenceBonus();
    int GetDexterityBonus();
    int GetLuckBonus();

    CombatStats Accept(IAttackVisitor visitor, Player player);
}