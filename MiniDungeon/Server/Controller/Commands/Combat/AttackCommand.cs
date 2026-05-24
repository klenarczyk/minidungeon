using MiniDungeon.Server.Controller.Commands.Core;
using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Combat;
using MiniDungeon.Server.Model.Combat.Attacks;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Shared.DTOs.Commands;
using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Server.Controller.Commands.Combat;

public class AttackCommand(AttackArgs args) : IServerCommand
{
    public bool Execute(IServerContext context)
    {
        var session = context.Session;
        var player = context.Player;
        
        var enemy = session.Entities.FirstOrDefault(e => e.BattledPlayerId == player.Id);
        if (enemy == null) return false;

        IAttackVisitor attackVisitor = args.Type switch
        {
            AttackType.Stealth => new StealthAttackVisitor(),
            AttackType.Magic => new MagicAttackVisitor(),
            _ => new NormalAttackVisitor()
        };

        var stats = GetCombatStats(player, attackVisitor);
        
        var playerDmg = enemy.TakeDamage(stats.Damage);
        if (enemy.IsDead)
        {
            Journal.Instance.Log($"{enemy.Name} is defeated!", player.Id, context.PlayerLogs);
            enemy.Die(session);
            player.IsBattling = false;
            return true;
        }
        
        var enemyDmg = EnemyAttack(enemy, player, stats.Defense);
        if (player.IsDead)
        {
            Journal.Instance.Log($"{player.Name} Died | Game Over!", player.Id, context.PlayerLogs);
            enemy.BattledPlayerId = null;
            context.IsRunning = false;
            return true;
        }

        Journal.Instance.Log($"P{player.Id}: {playerDmg}DMG, {player.Health}HP |" +
                             $" Enemy: {enemyDmg}DMG, {enemy.Health}HP", player.Id, context.PlayerLogs);

        return true;
    }

    private int EnemyAttack(IEntity enemyEntity, Player player, int defense)
    {
        var dmg = Math.Max(0, enemyEntity.DealDamage() - defense);
        player.Health -= dmg;
        return dmg;
    }

    private CombatStats GetCombatStats(Player player, IAttackVisitor attackVisitor)
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