namespace MiniDungeon.Shared.DTOs;

public record InventoryDto(List<ItemDto> Items, int Count, int Capacity);