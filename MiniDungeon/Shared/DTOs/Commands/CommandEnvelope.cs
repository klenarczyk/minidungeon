namespace MiniDungeon.Shared.DTOs.Commands;

public record CommandEnvelope(ActionType Type, string Payload = "");