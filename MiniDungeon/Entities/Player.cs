using MiniDungeon.Components;
using MiniDungeon.World;

namespace MiniDungeon.Entities;

public class Player
{
    public Position Position { get; set; }
    public Attributes Attributes { get; }
    public Inventory Inventory { get; } = new();
    public Equipment Equipment { get; } = new();
    public Purse Purse { get; } = new();

    public int Health { get; set; }
    public bool IsDead => Health <= 0;
    
    public int MaxHealth => Math.Max(1, Attributes.Health + Equipment.GetHealthBonus());
    public int Strength => Math.Max(0, Attributes.Strength + Equipment.GetStrengthBonus()); 
    public int Defense => Math.Max(0, Attributes.Defense + Equipment.GetDefenseBonus()); 
    public int Intelligence => Math.Max(0, Attributes.Intelligence + Equipment.GetIntelligenceBonus()); 
    public int Dexterity => Math.Max(0, Attributes.Dexterity + Equipment.GetDexterityBonus());
    public int Luck => Math.Max(0, Attributes.Luck + Equipment.GetLuckBonus());

    public Player(Position startingPosition)
    {
        Position = startingPosition;
        Attributes = new Attributes();
        Health = Attributes.Health;
    }
}