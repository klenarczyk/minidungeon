using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Combat;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class FleeParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope)
    {
        if (envelope.Type != ActionType.Flee) return base.Handle(envelope);
        return new FleeCommand();
    }
}