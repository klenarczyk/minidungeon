namespace MiniDungeon.Items;

public class WeaponItem(string name, int damage, bool isTwoHanded = false) : InventoryItem(name)
{
    public int Damage { get; } = damage;
    public bool IsTwoHanded { get; } = isTwoHanded;
}