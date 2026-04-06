using MiniDungeon.Items.Abstractions;

namespace MiniDungeon.Providers;

public interface IWeaponProvider
{
    IItem GetRandomWeapon();
}