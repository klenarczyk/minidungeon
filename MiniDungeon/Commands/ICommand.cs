using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public interface ICommand
{
    void Execute(GameSession session);
}