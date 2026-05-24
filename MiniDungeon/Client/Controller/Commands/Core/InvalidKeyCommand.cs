using MiniDungeon.Shared.Logging;

namespace MiniDungeon.Client.Controller.Commands.Core;

public class InvalidKeyCommand : IClientCommand
{
    public bool Execute(IClientContext context)
    {
        Journal.Instance.Log("Invalid key.");
        return true;
    }
}