using MiniDungeon.Input;
using MiniDungeon.World.Generation;

namespace MiniDungeon.Core;

public class Game : IGameContext
{
    public GameSession Session { get; }
    private readonly Renderer _renderer = new(120, 24);
    
    private readonly Stack<(IHandler Handler, string InputMode)> _inputChains = new();
    
    public void PushInputChain(IHandler chainHead, string inputMode = "") 
        => _inputChains.Push((chainHead, inputMode));
    public void PopInputChain() => _inputChains.Pop();

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
        _renderer.Render(Session);
        
        while (Session.IsRunning)
        {
            if (_inputChains.Count == 0) break;
            var currentChain = _inputChains.Peek().Handler;

            var key = Console.ReadKey(true).Key;
            var command = currentChain.Handle(key);
            command.Execute(this);
            
            Session.InputMode = _inputChains.Peek().InputMode;
            _renderer.Render(Session);
        }
    }
}