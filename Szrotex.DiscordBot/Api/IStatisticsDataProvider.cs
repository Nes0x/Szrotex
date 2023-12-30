namespace Szrotex.DiscordBot.Api;

public interface IStatisticsDataProvider
{
    Task<IEnumerable<string>> GetOnlinePlayersAsync();
}