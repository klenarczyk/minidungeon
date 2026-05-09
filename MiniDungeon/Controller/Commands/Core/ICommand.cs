namespace MiniDungeon.Controller.Commands.Core;

public interface ICommand
{
    /// <summary>
    /// <returns>true - time moved; false - time is frozen</returns>
    /// </summary>
    bool Execute(IGameContext context);
}