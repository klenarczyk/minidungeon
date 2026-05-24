using System.Text.Json;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Loot;

public class RequestDropCommand(int inventorySlot) : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var args = new DropArgs(inventorySlot);
        var payload = JsonSerializer.Serialize(args);
        
        var envelope = new CommandEnvelope(ActionType.Drop, payload);
        context.SendToServer(envelope);
        
        context.PopInputChain();
        return false;
    }
}