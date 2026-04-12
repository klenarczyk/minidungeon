using MiniDungeon.Loot.Items;

namespace MiniDungeon.Loot.Spawning;

public interface ILootProvider
{
    IItem GetRandomItem();
    IItem GetRandomWealth();
    IItem GetRandomWeapon();
}