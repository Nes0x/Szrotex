using System.Text.Json;

namespace Szrotex.DiscordBot.Discord.Config;

#nullable disable
public class BotConfig
{
    public string Status { get; init; }
    public string ApiUrl { get; init; }
    public string WssUrl { get; init; }
    public IdsConfig Ids { get; init; }
    public ColorConfig NormalColor { get; init; }
    public ColorConfig ErrorColor { get; init; }
    public MessagesConfig MessagesConfig { get; init; }

    public static BotConfig Create(string fileName)
    {
        return JsonSerializer.Deserialize<BotConfig>(File.ReadAllText(fileName));
    }
}