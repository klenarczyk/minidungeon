using System.Text.Json;
using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Controller.Commands.World;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class MoveParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope)
    {
        if (envelope.Type != ActionType.Move) return base.Handle(envelope);

        var args = JsonSerializer.Deserialize<MoveArgs>(envelope.Payload) ?? new MoveArgs();
        return new MoveCommand(args);
    }
}