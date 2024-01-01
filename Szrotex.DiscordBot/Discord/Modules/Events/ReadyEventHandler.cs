using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Handlers.Timers;
using Szrotex.DiscordBot.Handlers.Wss;

namespace Szrotex.DiscordBot.Discord.Modules.Events;

[GatewayEvent(nameof(GatewayClient.GuildCreate))]
public class ReadyEventHandler : IGatewayEventHandler<GuildCreateEventArgs>
{
    private readonly BotConfig _config;
    private readonly OnlinePlayersTimer _onlinePlayersTimer;
    private readonly BeamngChatWss _beamngChatWss;

    public ReadyEventHandler(BotConfig config, OnlinePlayersTimer onlinePlayersTimer, BeamngChatWss beamngChatWss)
    {
        _config = config;
        _onlinePlayersTimer = onlinePlayersTimer;
        _beamngChatWss = beamngChatWss;
    }

    public ValueTask HandleAsync(GuildCreateEventArgs arg)
    {
        if (arg.GuildId != _config.Ids.GuildId) return default;
        _onlinePlayersTimer.Start(TimeSpan.FromMinutes(1));
        _beamngChatWss.Start(_config.WssUrl);
        return default;
    }

}
