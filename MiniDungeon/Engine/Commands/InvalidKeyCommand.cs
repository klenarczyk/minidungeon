using MiniDungeon.Engine.Logging;

namespace MiniDungeon.Engine.Commands;

public class InvalidKeyCommand(string message = "Invalid key!") : ICommand
{
    public bool Execute(IGameContext context)
    {
        Journal.Instance.Log(message);
        return false;
    }
}