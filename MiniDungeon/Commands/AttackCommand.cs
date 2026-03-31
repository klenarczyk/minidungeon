using MiniDungeon.Core;
using MiniDungeon.Entities;
using MiniDungeon.World;

namespace MiniDungeon.Commands;

public class AttackCommand(Cell cell, int attackType) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
        var enemy = cell.Entity;

        if (enemy == null)
        {
            session.Message = string.Empty;
            context.PopInputChain();
            return;
        }
        
        PlayerAttack(player, enemy);
        if (enemy.IsDead)
        {
            session.Message = $"{enemy.Name} is defeated!";
            cell.Entity = null;
            context.PopInputChain();
            return;
        }
        
        EnemyAttack(enemy, player);
        if (player.IsDead)
        {
            session.Message = "You Died | Game Over!";
            session.IsRunning = false;
            context.PopInputChain();
            return;
        }

        session.Message = $"You: {player.Health}HP | Enemy: {enemy.Health}HP";
    }

    private void PlayerAttack(Player player, IEntity entity)
    {
        entity.TakeDamage(player.Strength);
    }

    private void EnemyAttack(IEntity entity, Player player)
    {
        player.Health -= entity.DealDamage();
    }
}