using MiniDungeon.Controller.Commands.Core;
using MiniDungeon.Core.Logging;
using MiniDungeon.Model.Actors;
using MiniDungeon.Model.Combat;
using MiniDungeon.Model.Combat.Attacks;
using MiniDungeon.Model.Loot;
using MiniDungeon.Model.Loot.Items;
using MiniDungeon.Model.World;

namespace MiniDungeon.Controller.Commands.Combat;

public class AttackCommand(Cell cell, IAttackVisitor attackVisitor) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = session.Players[0];
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