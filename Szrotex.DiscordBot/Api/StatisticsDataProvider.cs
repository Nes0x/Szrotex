using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Szrotex.DiscordBot.Api;

public class StatisticsDataProvider : IStatisticsDataProvider
{
    private readonly IApiWrapper _api;

    public StatisticsDataProvider(IApiWrapper api)
    {
        _api = api;
    }

    public async Task<IEnumerable<string>> GetOnlinePlayersAsync()
    {
        string encodedPlayersData = await _api.GetMethodAsync("statistics.php?online_list");
        var playersJsonArray = JsonSerializer.Deserialize<JsonArray>(encodedPlayersData);
        string[] players = playersJsonArray!.Root.AsArray().Select(value => value!.ToString()).ToArray();
        return players;
    }
}