using System.Timers;
using Timer = System.Timers.Timer;

namespace Szrotex.DiscordBot.Timers;

public abstract class TimerHandler : IDisposable
{
    private Timer? _timer;

    public void Start(TimeSpan timeSpan)
    {
        _timer = new Timer(timeSpan);
        _timer.Elapsed += DoAction;
        _timer.Start();
    }

    protected abstract void DoAction(object? sender, ElapsedEventArgs args);
    
    public void Dispose()
    {
        _timer?.Dispose();
    }
}