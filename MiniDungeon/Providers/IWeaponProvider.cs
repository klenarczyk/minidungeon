using MiniDungeon.Items;

namespace MiniDungeon.Providers;

public interface IWeaponProvider
{
    IItem GetRandomWeapon();
}