using MiniDungeon.Server.Logging;

namespace MiniDungeon.Server.Controller.Commands.Core;

public class InvalidKeyCommand(string message = "Invalid key!") : ICommand
{
    public bool Execute(IGameContext context)
    {
        Journal.Instance.Log(message);
        return false;
    }
}