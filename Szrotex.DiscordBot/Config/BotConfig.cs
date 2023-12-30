using System.Text.Json;

namespace Szrotex.DiscordBot.Config;

#nullable disable
public class BotConfig
{
    private const string ConfigFile = "appsettings.json";

    public string Status { get; init; }
    public string ApiUrl { get; init; }
    public IdsConfig Ids { get; init; }
    public ColorConfig NormalColor { get; init; }
    public ColorConfig ErrorColor { get; init; }
    public MessagesConfig MessagesConfig { get; init; }

    public static BotConfig Create()
    {
        return JsonSerializer.Deserialize<BotConfig>(File.ReadAllText(ConfigFile));
    }
}