using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Core;

public class RequestExitCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var envelope = new CommandEnvelope(ActionType.Exit);
        context.SendToServer(envelope);
        return false;
    }
}