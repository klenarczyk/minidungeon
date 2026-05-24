using System.Text.Json;
using MiniDungeon.Client.Controller.Commands.Core;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.World;

public class RequestMoveCommand(int deltaX, int deltaY) : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var args = new MoveArgs(deltaX, deltaY);
        var payload = JsonSerializer.Serialize(args);
        
        var envelope = new CommandEnvelope(ActionType.Move, payload);
        context.SendToServer(envelope);
        return false;
    }
}