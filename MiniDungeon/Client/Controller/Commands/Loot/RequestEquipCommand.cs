using System.Text.Json;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Loot;

public class RequestEquipCommand(EquipmentSlot equipmentSlot, int inventorySlot) : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var args = new EquipArgs(equipmentSlot, inventorySlot);
        var payload = JsonSerializer.Serialize(args);
        
        var envelope = new CommandEnvelope(ActionType.Equip, payload);
        context.SendToServer(envelope);
        
        context.PopInputChain();
        return false;
    }
}