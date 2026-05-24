namespace MiniDungeon.Server.Controller.Commands.Combat;

public class FleeCommand : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        var player = context.Player;
        if (!player.IsBattling) return false;

        var enemy = context.Session.Entities.FirstOrDefault(e => e.BattledPlayerId == player.Id);
        
        enemy?.BattledPlayerId = null;
        player.IsBattling = false;
        return false;
    }
}