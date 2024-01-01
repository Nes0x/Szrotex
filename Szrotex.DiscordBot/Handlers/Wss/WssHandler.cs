using WebSocketSharp;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Szrotex.DiscordBot.Handlers.Wss;

public abstract class WssHandler : IDisposable
{
    private WebSocket? _webSocket;

    public void Start(string url)
    {
        _webSocket = new WebSocket(url);
        _webSocket.OnMessage += OnMessage;
        _webSocket.OnClose += OnClose;
        _webSocket.OnError += OnError;
        _webSocket.OnOpen += OnOpen;
        _webSocket.Connect();
    }

    protected virtual void OnOpen(object? sender, EventArgs args)
    {
        
    }

    protected virtual void OnError(object? sender, ErrorEventArgs args)
    {
        
    }

    protected virtual void OnClose(object? sender, CloseEventArgs args)
    {
        
    }

    protected virtual void OnMessage(object? sender, MessageEventArgs args)
    {
    }
    
    

    
    public void Dispose()
    {
        _webSocket?.Close();
    }
}