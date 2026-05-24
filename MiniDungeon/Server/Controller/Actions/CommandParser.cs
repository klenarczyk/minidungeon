using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public abstract class CommandParser : IEnvelopeHandler
{
    private IEnvelopeHandler? _next;

    public IEnvelopeHandler SetNext(IEnvelopeHandler next)
    {
        _next = next;
        return next;
    }

    public virtual IServerCommand Handle(CommandEnvelope envelope)
        => _next != null ? _next.Handle(envelope) : new UnknownCommand();
}