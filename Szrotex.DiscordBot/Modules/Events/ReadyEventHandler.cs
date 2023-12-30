using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using Szrotex.DiscordBot.Api;
using Szrotex.DiscordBot.Config;
using Szrotex.DiscordBot.Creators;
using System.Timers;

namespace Szrotex.DiscordBot.Modules.Events;

[GatewayEvent(nameof(GatewayClient.GuildCreate))]
public class ReadyEventHandler : IGatewayEventHandler<GuildCreateEventArgs>
{
    private readonly IStatisticsDataProvider _statisticsDataProvider;
    private readonly GatewayClient _gatewayClient;
    private readonly BotConfig _config;
    private readonly IEmbedCreator _embedCreator;

    public ReadyEventHandler(GatewayClient gatewayClient, BotConfig config, IEmbedCreator embedCreator, IStatisticsDataProvider statisticsDataProvider)
    {
        _gatewayClient = gatewayClient;
        _embedCreator = embedCreator;
        _statisticsDataProvider = statisticsDataProvider;
        _config = config;
    }

    public ValueTask HandleAsync(GuildCreateEventArgs arg)
    {
        if (arg.GuildId != _config.Ids.GuildId) return default;
        var timer = new Timer(60000);
        timer.Elapsed += (sender, args) => UpdateEmbedWithOnlinePlayersAsync();
        timer.Start();
        return default;
    }

    private async Task UpdateEmbedWithOnlinePlayersAsync()
    {
        var players = (await _statisticsDataProvider.GetOnlinePlayersAsync()).ToArray();
        var formattedPlayers = players.Length != 0 ? string.Join(", ", players) : "brak"; 
        
        await _gatewayClient.Rest.ModifyMessageAsync(
            _config.Ids.OnlinePlayersChannelId,
            _config.Ids.OnlinePlayersMessageId,
            options => options.WithEmbeds(new [] {_embedCreator.CreateEmbed(_config.MessagesConfig.OnlinePlayersTitle, $"{_config.MessagesConfig.OnlinePlayersDescription} {formattedPlayers}")}));

    }
}
