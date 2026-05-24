using MiniDungeon.Client.Controller.Commands.Core;
using MiniDungeon.Client.Controller.Commands.Loot;
using MiniDungeon.Client.Controller.Commands.World;
using MiniDungeon.Client.Controller.Input;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Shared.DTOs;

namespace MiniDungeon.Client.Controller;

public static class ActionTranslator
{
    private static readonly Dictionary<ActionType, Action<InputChainBuilder>> Actions = new()
    {
        { ActionType.Exit, b => b.Bind(ConsoleKey.Escape, new RequestExitCommand(), "Quit (Esc)") },
        { ActionType.Journal, b => b.Bind(ConsoleKey.J, new ShowJournalCommand(), "Journal (J)") },
        { ActionType.Move, TranslateMove },
        { ActionType.Attack, TranslateAttack },
        { ActionType.Pickup, b => b.Bind(ConsoleKey.E, new RequestPickupCommand(), "Pick up (E)") },
        { ActionType.Drop, b => b.Bind(ConsoleKey.Q, new InitDropCommand(), "Drop (Q)") },
        { ActionType.Equip, TranslateEquip }
    };
    
    public static (IInputHandler BaseChain, string Instructions) Build(HandshakeDto handshake)
    {
        var builder = new InputChainBuilder();
        
        foreach (var action in handshake.AllowedActions)
        {
            if (Actions.TryGetValue(action, out var bindAction))
            {
                bindAction.Invoke(builder);
            }
        }

        builder.AddInstruction("Inventory (1-9)");
        
        return (builder.Head!, builder.Instructions);
    }

    // ---

    private static void TranslateMove(InputChainBuilder builder)
    {
        builder.Bind(ConsoleKey.W, new RequestMoveCommand(0, -1), "Move (WASD)");
        builder.Bind(ConsoleKey.A, new RequestMoveCommand(-1, 0));
        builder.Bind(ConsoleKey.S, new RequestMoveCommand(0, 1));
        builder.Bind(ConsoleKey.D, new RequestMoveCommand(1, 0));
    }
    
    private static void TranslateEquip(InputChainBuilder builder)
    {
        builder.Bind(ConsoleKey.L, new InitEquipCommand(EquipmentSlot.LeftHand), "Equip (L/R)");
        builder.Bind(ConsoleKey.R, new InitEquipCommand(EquipmentSlot.RightHand));
    }

    private static void TranslateAttack(InputChainBuilder builder)
    {
        builder.AddInstruction("Attack (1-Normal 2-Stealth 3-Magic)");
    }
}