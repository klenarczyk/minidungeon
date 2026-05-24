using MiniDungeon.Server.Model.Loot;

namespace MiniDungeon.Shared.DTOs.Commands;

public record EquipArgs(EquipmentSlot EquipmentSlot, int InventorySlot);