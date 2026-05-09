namespace MiniDungeon.Server.Model.Loot;

public class Purse
{
    public int Gold { get; private set; } = 0;
    public int Coins { get; private set; } = 0;
    
    public bool TryAddGold(int amount)
    {
        if (amount < 0) return false;
        Gold += amount;
        return true;
    }

    public bool TryRemoveGold(int amount)
    {
        if (amount < 0 || amount > Gold) return false;
        Gold -= amount;
        return true;
    }
    
    public bool TryAddCoins(int amount)
    {
        if (amount < 0) return false;
        Coins += amount;
        return true;
    }

    public bool TryRemoveCoins(int amount)
    {
        if (amount < 0 || amount > Coins) return false;
        Coins -= amount;
        return true;
    }
}