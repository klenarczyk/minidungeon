namespace MiniDungeon.Shared.DTOs;

public record PlayerDto(
    int PlayerId,
    int X,
    int Y,
    AttributesDto Attributes,
    InventoryDto Inventory,
    EquipmentDto Equipment,
    PurseDto Purse);