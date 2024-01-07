using System.Timers;
using Szrotex.DiscordBot.Api;
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Discord.Embed;

namespace Szrotex.DiscordBot.Handlers.Timers;

public class OnlinePlayersTimer : TimerHandler
{
    private readonly BotConfig _config;
    private readonly EmbedCreator _embedCreator;
    private readonly EmbedModifier _embedModifier;
    private readonly StatisticsDataProvider _statisticsDataProvider;
    private const int ServerSlots = 10;

    public OnlinePlayersTimer(StatisticsDataProvider statisticsDataProvider, BotConfig config,
        EmbedCreator embedCreator, EmbedModifier embedModifier)
    {
        _statisticsDataProvider = statisticsDataProvider;
        _config = config;
        _embedCreator = embedCreator;
        _embedModifier = embedModifier;
    }

    protected override void Execute(object? sender, ElapsedEventArgs args)
    {
        UpdateEmbedWithOnlinePlayersAsync();
    }


    private async Task UpdateEmbedWithOnlinePlayersAsync()
    {
        var messages = _config.Messages;
        var ids = _config.Ids;
        
        string[] players = (await _statisticsDataProvider.FetchOnlinePlayersAsync()).ToArray();
        string formattedPlayers = players.Length != 0 ? string.Join(", ", players) : messages.NullPlayers;
        
        var toModify = _embedCreator.Create($"{messages.OnlinePlayersTitle} - {players.Length}/{ServerSlots}",
            $"{messages.OnlinePlayersDescription} {formattedPlayers}");
        await _embedModifier.ModifyAsync(ids.OnlinePlayersChannelId, ids.OnlinePlayersMessageId, toModify);
    }
}