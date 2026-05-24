using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Model.Actors;

public class Player
{
    public int Id { get; init; }
    public string Name { get; set; } = "Player";
    public Position Position { get; set; }
    public Attributes BaseAttributes { get; }
    public Inventory Inventory { get; } = new();
    public Equipment Equipment { get; } = new();
    public Purse Purse { get; } = new();

    public int Health { get; set; }
    public bool IsDead => Health <= 0;
    
    public int MaxHealth => Math.Max(1, BaseAttributes.Health + Equipment.GetHealthBonus());
    public int Strength => Math.Max(0, BaseAttributes.Strength + Equipment.GetStrengthBonus()); 
    public int Defense => Math.Max(0, BaseAttributes.Defense + Equipment.GetDefenseBonus()); 
    public int Intelligence => Math.Max(0, BaseAttributes.Intelligence + Equipment.GetIntelligenceBonus()); 
    public int Dexterity => Math.Max(0, BaseAttributes.Dexterity + Equipment.GetDexterityBonus());
    public int Luck => Math.Max(0, BaseAttributes.Luck + Equipment.GetLuckBonus());

    public bool IsBattling { get; set; } = false;
    
    public Player(Position startingPosition)
    {
        Position = startingPosition;
        BaseAttributes = new Attributes();
        Health = BaseAttributes.Health;
    }
}