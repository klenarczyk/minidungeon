using System.Text.Json;
using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Combat;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class AttackParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope)
    {
        if (envelope.Type != ActionType.Attack) return base.Handle(envelope);

        var args = JsonSerializer.Deserialize<AttackArgs>(envelope.Payload);
        return new AttackCommand(args);
    }
}