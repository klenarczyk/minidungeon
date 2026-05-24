namespace MiniDungeon.Shared.DTOs.State;

public record GameStateDto(
    PlayerDto Player,
    List<string> JournalLogs,
    
    List<PositionDto> Walls,
    List<ItemDto> Items,
    List<ActorDto> Enemies,
    Dictionary<int, ActorDto> Players
    );