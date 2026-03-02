using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class SelectItemCommand(int idx) : ICommand
{
    public void Execute(GameSession session)
    {
        var inv = session.Player.Inventory;

        if (idx < 0 || idx >= inv.Count) return;
        
        inv.SelectedSlot = idx;
    }
}