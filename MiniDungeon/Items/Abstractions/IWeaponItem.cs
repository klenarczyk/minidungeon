namespace MiniDungeon.Items.Abstractions;

public interface IWeaponItem : IInventoryItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
}