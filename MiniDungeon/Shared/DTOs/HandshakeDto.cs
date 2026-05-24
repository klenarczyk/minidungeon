namespace MiniDungeon.Shared.DTOs;

public class HandshakeDto
{
    public List<ActionType> AllowedActions { get; set; } = [];
    public string EntryMessage { get; set; } = string.Empty;
}