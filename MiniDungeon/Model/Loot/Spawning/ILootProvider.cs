using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.Loot.Items.Weapons;

namespace MiniDungeon.Model.Loot.Spawning;

public interface ILootProvider
{
    IItem GetRandomItem();
    IItem GetRandomWealth();
    IWeaponItem GetRandomWeapon();
}