using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using Szrotex.DiscordBot.Config;
using Szrotex.DiscordBot.Timers;

namespace Szrotex.DiscordBot.Modules.Events;

[GatewayEvent(nameof(GatewayClient.GuildCreate))]
public class ReadyEventHandler : IGatewayEventHandler<GuildCreateEventArgs>
{
    private readonly BotConfig _config;
    private readonly OnlinePlayersTimer _onlinePlayersTimer;

    public ReadyEventHandler(BotConfig config, OnlinePlayersTimer onlinePlayersTimer)
    {
        _config = config;
        _onlinePlayersTimer = onlinePlayersTimer;
    }

    public ValueTask HandleAsync(GuildCreateEventArgs arg)
    {
        if (arg.GuildId != _config.Ids.GuildId) return default;
        _onlinePlayersTimer.Start(TimeSpan.FromMinutes(1));
        return default;
    }

}
