namespace MiniDungeon.Shared.DTOs;

public record GameStateDto(
    PlayerDto Player,
    string InputMode,
    string Message,
    string Instructions,
    List<string> JournalLogs,
    bool ShowJournal,
    
    List<PositionDto> Walls,
    List<ItemDto> Items,
    List<ActorDto> Enemies,
    Dictionary<int, ActorDto> Players
    );