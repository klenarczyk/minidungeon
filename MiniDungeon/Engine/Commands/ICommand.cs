namespace MiniDungeon.Engine.Commands;

public interface ICommand
{
    void Execute(IGameContext context);
}