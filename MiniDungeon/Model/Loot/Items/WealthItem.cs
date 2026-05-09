using MiniDungeon.Model.Actors;
using MiniDungeon.Model.World;

namespace MiniDungeon.Model.Loot.Items;

public class WealthItem(string name, Action<Purse, int>? addToPurse = null) : IItem
{
    public string Name { get; } = name;
    
    public bool Collect(Player player, Cell cell)
    {
        if (!cell.TryRemoveItem(this)) return false;
        
        AddToPurse(player.Purse);
        return true;
    }

    private void AddToPurse(Purse purse, int amount = 1)
    {
        addToPurse?.Invoke(purse, amount);
    }
}