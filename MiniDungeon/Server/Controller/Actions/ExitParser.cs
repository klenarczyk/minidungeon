using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public class ExitParser : CommandParser
{
    public override IServerCommand Handle(CommandEnvelope envelope) 
        => envelope.Type != ActionType.Exit ? base.Handle(envelope) : new ExitCommand();
}