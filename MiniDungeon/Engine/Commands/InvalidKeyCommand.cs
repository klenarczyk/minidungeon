namespace MiniDungeon.Engine.Commands;

public class InvalidKeyCommand(string message = "Invalid key!") : ICommand
{
    public void Execute(IGameContext context)
    {
        context.Session.Message = message;
    }
}