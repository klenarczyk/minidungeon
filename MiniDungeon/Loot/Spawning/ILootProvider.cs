using MiniDungeon.Loot.Items;
using MiniDungeon.Loot.Items.Weapons;

namespace MiniDungeon.Loot.Spawning;

public interface ILootProvider
{
    IItem GetRandomItem();
    IItem GetRandomWealth();
    IWeaponItem GetRandomWeapon();
}