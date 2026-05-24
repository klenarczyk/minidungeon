using System.Text.Json;
using MiniDungeon.Shared.DTOs;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client.Controller.Commands.Combat;

public class RequestAttackCommand(AttackType attackType) : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        var args = new AttackArgs(attackType);
        var payload = JsonSerializer.Serialize(args);

        var enveloper = new CommandEnvelope(ActionType.Attack, payload);
        context.SendToServer(enveloper);
        return false;
    }
}