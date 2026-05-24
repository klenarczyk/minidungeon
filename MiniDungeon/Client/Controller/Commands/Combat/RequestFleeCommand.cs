using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Combat;

public class RequestFleeCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var envelope = new CommandEnvelope(ActionType.Flee);
        context.SendToServer(envelope);
        // context.PopInputChain(); // Done in client loop
        return false;
    }
}