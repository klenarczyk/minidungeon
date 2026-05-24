namespace MiniDungeon.Client.Controller.Commands;

public interface IClientCommand
{
    /// <summary>
    /// <returns>true - update UI; otherwise false</returns>
    /// </summary>
    bool Execute(IClientContext context);
}