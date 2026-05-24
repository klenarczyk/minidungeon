using System.Text.Json;
using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Loot;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class EquipParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope)
    {
        if (envelope.Type != ActionType.Equip) return base.Handle(envelope);

        var args = JsonSerializer.Deserialize<EquipArgs>(envelope.Payload);
        return new EquipCommand(args);
    }
}