using MiniDungeon.Core;

namespace MiniDungeon.Commands;

public class InvalidKeyCommand(string message = "Invalid key!") : ICommand
{
    public void Execute(GameSession session)
    {
        session.Message = message;
    }
}