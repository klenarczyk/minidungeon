namespace MiniDungeon.Shared.DTOs.State;

public record AttributesDto(
    int Health,
    int MaxHealth,
    int Strength,
    int Defense,
    int Intelligence,
    int Dexterity,
    int Luck
    );