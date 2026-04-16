using MiniDungeon.Engine.Configuration;
using MiniDungeon.Engine.Input;
using MiniDungeon.Engine.Persistence;
using MiniDungeon.Engine.UI;
using MiniDungeon.World.Generation;
using MiniDungeon.World.Themes;

namespace MiniDungeon.Engine;

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
        var configPath = Path.Combine(AppContext.BaseDirectory, "Engine", "Configuration", "config.json");
        var config = ConfigLoader.Load(configPath);
        
        var logger = new FileLogger(config);
        Journal.Initialize(logger);
        
        IDungeonTheme theme = new WorkshopTheme();
        
        var layoutBuilder = new LayoutBuilder(theme);
        var instructionBuilder = new InstructionBuilder();
        
        theme.GenerationTemplate.Use(layoutBuilder);
        theme.GenerationTemplate.Use(instructionBuilder);

        var baseInputChain = instructionBuilder.GetInputChain();
        if (baseInputChain != null)
        {
            PushInputChain(baseInputChain);
            _renderer.UpdateInstructions(instructionBuilder.GetInstructions());
        }

        Session = new GameSession(layoutBuilder.Build())
        {
            Message = theme.EntryMessage
        };
        Session.Player.Name = config.PlayerName;
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
            
            if (Journal.Instance.Entries.Count > 0)
                Session.Message = Journal.Instance.Entries[^1];
            
            Session.InputMode = _inputChains.Peek().InputMode;
            _renderer.Render(Session);
        }
    }
}