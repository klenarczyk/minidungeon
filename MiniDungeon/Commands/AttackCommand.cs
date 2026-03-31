using MiniDungeon.Combat;
using MiniDungeon.Components;
using MiniDungeon.Core;
using MiniDungeon.Entities;
using MiniDungeon.Items;
using MiniDungeon.World;

namespace MiniDungeon.Commands;

public class AttackCommand(Cell cell, IAttackVisitor attackVisitor) : ICommand
{
    public void Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
        var enemy = cell.Entity;
        var stats = GetCombatStats(player);
        
        if (enemy == null)
        {
            session.Message = string.Empty;
            context.PopInputChain();
            return;
        }
        
        enemy.TakeDamage(stats.Damage);
        if (enemy.IsDead)
        {
            session.Message = $"{enemy.Name} is defeated!";
            cell.Entity = null;
            context.PopInputChain();
            return;
        }
        
        EnemyAttack(enemy, player, stats.Defense);
        if (player.IsDead)
        {
            session.Message = "You Died | Game Over!";
            session.IsRunning = false;
            context.PopInputChain();
            return;
        }

        session.Message = $"You: {player.Health}HP | Enemy: {enemy.Health}HP";
    }

    private void EnemyAttack(IEntity enemy, Player player, int defense)
    {
        var dmg = Math.Max(0, enemy.DealDamage() - defense);
        player.Health -= dmg;
    }

    private CombatStats GetCombatStats(Player player)
    {
        var item = player.Equipment[EquipmentSlot.LeftHand] 
                    ?? player.Equipment[EquipmentSlot.RightHand] 
                    ?? new InventoryItem("fist");

        var stats = item.Accept(attackVisitor, player);
        
        return new CombatStats(
            Math.Max(0, stats.Damage),
            Math.Max(0, stats.Defense)
        );
    }
}