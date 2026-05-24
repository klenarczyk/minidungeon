namespace MiniDungeon.Shared.DTOs.State;

public record InventoryDto(List<ItemDto> Items, int Count, int Capacity);