using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Combat;
using MiniDungeon.Server.Model.Combat.Attacks;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Model.Loot.Items;

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