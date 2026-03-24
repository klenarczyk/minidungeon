using MiniDungeon.Input;
using MiniDungeon.World.Generation;

namespace MiniDungeon.Core;

public class Game : IGameContext
{
    public GameSession Session { get; }
    private readonly Renderer _renderer = new(80, 24);
    
    private readonly Stack<IHandler> _inputChains = new();
    
    public void PushInputChain(IHandler chainHead) =>  _inputChains.Push(chainHead);
    public void PopInputChain() =>  _inputChains.Pop();

    public Game()
    {
        var dungeonBuilder = new DungeonBuilder();
        var instructionBuilder = new InstructionBuilder();
        var director = new DungeonDirector();
        
        director.CreateStandardDungeon(dungeonBuilder);
        director.CreateStandardDungeon(instructionBuilder);

        var baseInputChain = instructionBuilder.GetInputChain();
        if (baseInputChain != null)
        {
            PushInputChain(baseInputChain);
            _renderer.UpdateInstructions(instructionBuilder.GetInstructions());
        }
    
        Session = new GameSession(dungeonBuilder.Build());
    }
    
    public void Run()
    {
        _renderer.Init();

        while (Session.IsRunning)
        {
            _renderer.Render(Session);

            if (_inputChains.Count == 0) Session.IsRunning = false;
            var currentChain = _inputChains.Peek();

            var key = Console.ReadKey(true).Key;
            var command = currentChain.Handle(key);
            command.Execute(this);
        }
    }
}