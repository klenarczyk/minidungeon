using MiniDungeon.Loot.Items;

namespace MiniDungeon.Loot.Spawning;

public interface IWeaponProvider
{
    IItem GetRandomWeapon();
}