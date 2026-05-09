using MiniDungeon.Controller.Input;
using MiniDungeon.Core.Configuration;
using MiniDungeon.Core.Logging;
using MiniDungeon.Model;
using MiniDungeon.Model.Actors;
using MiniDungeon.Model.World.Generation;
using MiniDungeon.Model.World.Themes;
using MiniDungeon.View;

namespace MiniDungeon.Controller;

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
        var configPath = Path.Combine(AppContext.BaseDirectory, "Core", "Configuration", "config.json");
        var config = ConfigLoader.Load(configPath);
        
        var logger = new FileLogger(config);
        Journal.Initialize(logger);
        
        IDungeonTheme theme = new WorkshopTheme();

        var entityList = new List<IEntity>();
        var layoutBuilder = new LayoutBuilder(theme, entityList);
        var instructionBuilder = new InstructionBuilder();
        
        theme.GenerationTemplate.Use(layoutBuilder);
        theme.GenerationTemplate.Use(instructionBuilder);

        var baseInputChain = instructionBuilder.GetInputChain();
        if (baseInputChain != null)
        {
            PushInputChain(baseInputChain);
            _renderer.UpdateInstructions(instructionBuilder.GetInstructions());
        }

        var board = layoutBuilder.Build();
        
        Session = new GameSession(board)
        {
            Message = theme.EntryMessage,
            Entities = entityList
        };
        
        Session.Players.Add(new Player(board.StartingPosition) { Name = config.PlayerName });
    }
    
    public void Run()
    {
        _renderer.Init();
        _renderer.Render(Session);

        try
        {
            while (Session.IsRunning)
            {
                if (_inputChains.Count == 0) break;
                var currentChain = _inputChains.Peek().Handler;

                var key = Console.ReadKey(true).Key;
                var command = currentChain.Handle(key);
                var timePassed = command.Execute(this);

                if (timePassed)
                {
                    foreach (var entity in Session.Entities)
                    {
                        entity.Move(this);
                    }
                }

                if (Journal.Instance.Entries.Count > 0)
                    Session.Message = Journal.Instance.Entries[^1];

                Session.InputMode = _inputChains.Peek().InputMode;
                _renderer.Render(Session);
            }
        }
        finally
        {
            Journal.Instance.OnExit();   
        }
    }
}