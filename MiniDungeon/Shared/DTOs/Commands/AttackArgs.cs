namespace MiniDungeon.Shared.DTOs.Commands;

public enum AttackType
{
    Normal,
    Stealth,
    Magic
}

public record AttackArgs(AttackType Type);