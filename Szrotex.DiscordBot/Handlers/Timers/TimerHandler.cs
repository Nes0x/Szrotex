using System.Timers;
using Timer = System.Timers.Timer;

namespace Szrotex.DiscordBot.Handlers.Timers;

public abstract class TimerHandler : IDisposable
{
    private Timer? _timer;

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public void Start(TimeSpan timeSpan)
    {
        _timer = new Timer(timeSpan);
        _timer.Elapsed += Execute;
        _timer.Start();
    }

    protected abstract void Execute(object? sender, ElapsedEventArgs args);
}