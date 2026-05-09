using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Combat;
using MiniDungeon.Model.Combat.Attacks;
using MiniDungeon.Model.World;

namespace MiniDungeon.Model.Loot.Items;

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