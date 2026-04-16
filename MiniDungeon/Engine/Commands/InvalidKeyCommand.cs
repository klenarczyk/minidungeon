using MiniDungeon.Engine.Logging;

namespace MiniDungeon.Engine.Commands;

public class InvalidKeyCommand(string message = "Invalid key!") : ICommand
{
    public void Execute(IGameContext context)
    {
        Journal.Instance.Log(message);
    }
}