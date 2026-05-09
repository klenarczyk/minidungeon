using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Weapons;

namespace MiniDungeon.Server.Model.Loot.Spawning;

public interface ILootProvider
{
    IItem GetRandomItem();
    IItem GetRandomWealth();
    IWeaponItem GetRandomWeapon();
}