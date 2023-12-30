using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using Szrotex.DiscordBot.Api;
using Szrotex.DiscordBot.Config;
using Szrotex.DiscordBot.Creators;

namespace Szrotex.DiscordBot.Modules.Events;

[GatewayEvent(nameof(GatewayClient.GuildCreate))]
public class ReadyEventHandler : IGatewayEventHandler<GuildCreateEventArgs>
{
    private readonly IStatisticsDataProvider _statisticsDataProvider;
    private readonly GatewayClient _gatewayClient;
    private readonly IdsConfig _idsConfig;
    private readonly IEmbedCreator _embedCreator;

    public ReadyEventHandler(GatewayClient gatewayClient, BotConfig botConfig, IEmbedCreator embedCreator, IStatisticsDataProvider statisticsDataProvider)
    {
        _gatewayClient = gatewayClient;
        _embedCreator = embedCreator;
        _statisticsDataProvider = statisticsDataProvider;
        _idsConfig = botConfig.Ids;
    }

    public ValueTask HandleAsync(GuildCreateEventArgs arg)
    {
        if (arg.GuildId != 995654775297294386) return default;
        var timer = new Timer(_ => UpdateEmbedWithOnlinePlayersAsync(), null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(60));
        return default;
    }

    private async Task UpdateEmbedWithOnlinePlayersAsync()
    {
        var players = (await _statisticsDataProvider.GetOnlinePlayersAsync()).ToArray();
        var formattedPlayers = players.Length != 0 ? string.Join(", ", players) : "brak"; 
        
        await _gatewayClient.Rest.ModifyMessageAsync(
            _idsConfig.OnlinePlayersChannelId,
            _idsConfig.OnlinePlayersMessageId,
            options => options.WithEmbeds(new [] {_embedCreator.CreateEmbed("Gracze online", $"Osoby obecne na serwerze: {formattedPlayers}")}));

    }
}