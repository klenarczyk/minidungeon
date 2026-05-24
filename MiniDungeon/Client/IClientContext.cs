using MiniDungeon.Client.Controller.Input;
using MiniDungeon.Shared.DTOs.Commands;

namespace MiniDungeon.Client;

public interface IClientContext
{
    bool ShowJournal { get; set; }
    string InputMode { get; }
    string EntryMessage { get; }
    
    void PushInputChain(IInputHandler chain, string mode = "");
    void PopInputChain();
    
    void SendToServer(CommandEnvelope envelope);
}