namespace MiniDungeon.Shared.DTOs;

public record AttributesDto(
    int Health,
    int MaxHealth,
    int Strength,
    int Defense,
    int Intelligence,
    int Dexterity,
    int Luck
    );