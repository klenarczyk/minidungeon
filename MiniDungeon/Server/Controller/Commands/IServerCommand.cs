namespace MiniDungeon.Server.Controller.Commands;

public interface IServerCommand
{
    /// <summary>
    /// <returns>true - time moved; false - time is frozen</returns>
    /// </summary>
    bool Execute(IServerContext context);
}