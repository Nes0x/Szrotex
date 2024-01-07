using System.Text.Json;
using Szrotex.DiscordBot.Models;

namespace Szrotex.DiscordBot.Discord.Config;

#nullable disable
public class BotConfig
{
    private static readonly FileType ConfigFile = new("appsettings", "json");
    private static readonly FileType MessagesFile = new("messages", "json");
    
    
    public string Status { get; init; }
    public string ApiUrl { get; init; }
    public string WssUrl { get; init; }
    public IdsConfig Ids { get; private set; }
    public ColorConfig BaseColor { get; init; }
    public ColorConfig ErrorColor { get; init; }
    public MessagesConfig Messages { get; private set; }

    public static BotConfig Create(FileType ids)
    {
        var botConfig = JsonSerializer.Deserialize<BotConfig>(File.ReadAllText(ConfigFile.AsPath()));
        botConfig.Messages = JsonSerializer.Deserialize<MessagesConfig>(File.ReadAllText(MessagesFile.AsPath()));
        botConfig.Ids = JsonSerializer.Deserialize<IdsConfig>(File.ReadAllText(ids.AsPath()));
        return botConfig;
    }
}