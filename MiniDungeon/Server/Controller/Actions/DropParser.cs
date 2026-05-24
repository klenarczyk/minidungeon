using System.Text.Json;
using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Loot;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class DropParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope)
    {
        if (envelope.Type != ActionType.Drop) return base.Handle(envelope);
        
        var args = JsonSerializer.Deserialize<DropArgs>(envelope.Payload);
        return new DropCommand(args);
    }
}