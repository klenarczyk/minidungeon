using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Loot;

public class RequestPickupCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var envelope = new CommandEnvelope(ActionType.Pickup);
        context.SendToServer(envelope);
        return false;
    }
}