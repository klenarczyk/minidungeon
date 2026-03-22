using MiniDungeon.Input;
using MiniDungeon.World.Generation;

namespace MiniDungeon.Core;

public class Game
{
    private readonly GameSession _session;
    private readonly IHandler? _inputChain;
    private readonly Renderer _renderer = new(80, 24);

    public Game()
    {
        var dungeonBuilder = new DungeonBuilder();
        var instructionBuilder = new InstructionBuilder();
        var director = new DungeonDirector();
        
        director.CreateStandardDungeon(dungeonBuilder);
        director.CreateStandardDungeon(instructionBuilder);
        
        _inputChain = instructionBuilder.GetInputChain();
        _renderer.UpdateInstructions(instructionBuilder.GetInstructions());
        
        _session = new GameSession(dungeonBuilder.Build());
    }
    
    public void Run()
    {
        _renderer.Init();

        while (_session.IsRunning)
        {
            _renderer.Render(_session);
            
            var command = _inputChain?.Handle(Console.ReadKey(true).Key);
            command?.Execute(_session);
        }
    }
}