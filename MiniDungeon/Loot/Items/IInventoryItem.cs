using MiniDungeon.Actors;
using MiniDungeon.Combat;
using MiniDungeon.Combat.Attacks;
using MiniDungeon.World;

namespace MiniDungeon.Loot.Items;

public interface IInventoryItem : IItem
{
    bool Collect(Player player, Cell cell, IInventoryItem item);
    bool Equip(Player player, EquipmentSlot slot = EquipmentSlot.LeftHand);

    int GetHealthBonus();
    int GetStrengthBonus();
    int GetDefenseBonus();
    int GetIntelligenceBonus();
    int GetDexterityBonus();
    int GetLuckBonus();

    CombatStats Accept(IAttackVisitor visitor, Player player);
}