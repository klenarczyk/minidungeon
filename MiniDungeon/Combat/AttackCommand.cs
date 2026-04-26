using MiniDungeon.Actors;
using MiniDungeon.Combat.Attacks;
using MiniDungeon.Engine;
using MiniDungeon.Engine.Commands;
using MiniDungeon.Engine.Logging;
using MiniDungeon.Loot;
using MiniDungeon.Loot.Items;
using MiniDungeon.World;

namespace MiniDungeon.Combat;

public class AttackCommand(Cell cell, IAttackVisitor attackVisitor) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Player;
        var enemy = cell.Entity;
        var stats = GetCombatStats(player);
        
        if (enemy == null)
        {
            session.Message = string.Empty;
            context.PopInputChain();
            return false;
        }
        
        var playerDmg = enemy.TakeDamage(stats.Damage);
        if (enemy.IsDead)
        {
            Journal.Instance.Log($"{enemy.Name} is defeated!");
            enemy.Die(session);
            context.PopInputChain();
            return false;
        }
        
        var enemyDmg = EnemyAttack(enemy, player, stats.Defense);
        if (player.IsDead)
        {
            Journal.Instance.Log($"{player.Name} Died | Game Over!");
            session.IsRunning = false;
            context.PopInputChain();
            return false;
        }

        Journal.Instance.Log($"{player.Name}: {playerDmg}DMG, {player.Health}HP |" +
                             $" Enemy: {enemyDmg}DMG, {enemy.Health}HP");

        return false;
    }

    private int EnemyAttack(IEntity enemy, Player player, int defense)
    {
        var dmg = Math.Max(0, enemy.DealDamage() - defense);
        player.Health -= dmg;
        return dmg;
    }

    private CombatStats GetCombatStats(Player player)
    {
        var item = player.Equipment[EquipmentSlot.LeftHand] 
                    ?? player.Equipment[EquipmentSlot.RightHand] 
                    ?? new InventoryItem("fists");

        var stats = item.Accept(attackVisitor, player);
        
        return new CombatStats(
            Math.Max(0, stats.Damage),
            Math.Max(0, stats.Defense)
        );
    }
}