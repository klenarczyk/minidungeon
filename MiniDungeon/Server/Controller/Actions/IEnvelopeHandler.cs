using MiniDungeon.Server.Controller.Commands;
using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Server.Controller.Actions;

public interface IEnvelopeHandler
{
    IEnvelopeHandler SetNext(IEnvelopeHandler handler);
    IServerCommand Handle(CommandEnvelope envelope);
}