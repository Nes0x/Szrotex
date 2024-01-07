﻿using System.Drawing;
using NetCord.Rest;
using Szrotex.DiscordBot.Discord.Config;
using Color = NetCord.Color;

namespace Szrotex.DiscordBot.Discord.Embed;

using Color = Color;

public class EmbedCreator
{
    private readonly BotConfig _config;

    public EmbedCreator(BotConfig config)
    {
        _config = config;
    }

    public EmbedProperties Create(string title, string description, string? color = null)
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
            .WithTitle(title)
            .WithDescription(description)
            .WithColor(discordColor)
            .WithFooter(new EmbedFooterProperties().WithText(_config.MessagesConfig.FooterEmbed))
            .WithTimestamp(DateTimeOffset.UtcNow);

        return embed;
    }

    public EmbedProperties CreateError(string description)
    {
        return Create(_config.MessagesConfig.ErrorTitle, description, "red");
    }
}