using MiniDungeon.Client.Controller.Commands;

namespace MiniDungeon.Client.Controller.Input;

public class InputChainBuilder
{
    public IInputHandler? Head { get; private set; }
    private IInputHandler? _tail;
    
    private readonly List<string> _instructions = [];
    public string Instructions => string.Join(", ", _instructions);

    public void Bind(ConsoleKey key, IClientCommand command, string instructionText = "")
    {
        var handler = new InputHandler(key, command);

        if (Head == null)
        {
            Head = handler;
            _tail = handler;
        }
        else
        {
            _tail = _tail!.SetNext(handler);
        }

        if (!string.IsNullOrWhiteSpace(instructionText))
        {
            _instructions.Add(instructionText);
        }
    }
    
    public void AddInstruction(string text) => _instructions.Add(text);
}