namespace MiniDungeon.Shared.DTOs.State;

public record PlayerDto(
    int PlayerId,
    int X,
    int Y,
    AttributesDto Attributes,
    InventoryDto Inventory,
    EquipmentDto Equipment,
    PurseDto Purse,
    bool IsBattling);