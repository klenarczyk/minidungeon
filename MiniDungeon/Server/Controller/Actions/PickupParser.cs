using System.Text.Json;
using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Loot;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class PickupParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope)
        => envelope.Type != ActionType.Pickup ? base.Handle(envelope) : new PickUpCommand();
}