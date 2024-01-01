using System.Drawing;
using NetCord.Rest;
using Szrotex.DiscordBot.Config;

namespace Szrotex.DiscordBot.Embed;
using Color = NetCord.Color;

public class EmbedCreator
{
    private readonly BotConfig _config;

    public EmbedCreator(BotConfig config)
    {
        _config = config;
    }

    public EmbedProperties CreateEmbed(string title, string description, string? color = null)
    {
        var discordColor = new Color(_config.NormalColor.R, _config.NormalColor.G, _config.NormalColor.B);
        if (!string.IsNullOrWhiteSpace(color))
        {
            var colorFromName = (System.Drawing.Color)(new ColorConverter().ConvertFromString(color) ??
                                                       throw new InvalidOperationException(
                                                           "This color isn't exists..."));
            discordColor = new Color(colorFromName.R, colorFromName.G, colorFromName.B);
        }

        var embed = new EmbedProperties()
            .AddFields()
            .WithTitle(title)
            .WithDescription(description)
            .WithColor(discordColor)
            .WithFooter(new EmbedFooterProperties().WithText(_config.MessagesConfig.FooterEmbed))
            .WithTimestamp(DateTimeOffset.UtcNow);

        return embed;
    }

    public EmbedProperties CreateErrorEmbed(string description)
    {
        return CreateEmbed(_config.MessagesConfig.ErrorTitle, description, "red");
    }
    
    
}