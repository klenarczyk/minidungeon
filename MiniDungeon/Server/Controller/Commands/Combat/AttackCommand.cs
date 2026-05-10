using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Logging;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Combat;
using MiniDungeon.Server.Model.Combat.Attacks;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Server.Controller.Commands.Combat;

public class AttackCommand(IEntity? enemy, IAttackVisitor attackVisitor) : ICommand
{
    public bool Execute(IGameContext context)
    {
        var session = context.Session;
        var player = context.Player;
        var stats = GetCombatStats(player);
        
        if (enemy == null)
        {
            context.PopInputChain();
            return false;
        }
        
        var playerDmg = enemy.TakeDamage(stats.Damage);
        if (enemy.IsDead)
        {
            Journal.Instance.Log($"{enemy.Name} is defeated!", player.Id, context.PlayerLogs);
            enemy.Die(session);
            context.PopInputChain();
            return false;
        }
        
        var enemyDmg = EnemyAttack(enemy, player, stats.Defense);
        if (player.IsDead)
        {
            Journal.Instance.Log($"{player.Name} Died | Game Over!", player.Id, context.PlayerLogs);
            enemy.BattledPlayerId = null;
            session.IsRunning = false;
            context.PopInputChain();
            return false;
        }

        Journal.Instance.Log($"P{player.Id}: {playerDmg}DMG, {player.Health}HP |" +
                             $" Enemy: {enemyDmg}DMG, {enemy.Health}HP", player.Id, context.PlayerLogs);

        return false;
    }

    private int EnemyAttack(IEntity enemyEntity, Player player, int defense)
    {
        var dmg = Math.Max(0, enemyEntity.DealDamage() - defense);
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