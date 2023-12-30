using NetCord.Rest;

namespace Szrotex.DiscordBot.Creators;

public interface IEmbedCreator
{
    EmbedProperties CreateEmbed(string title, string description, string? color = null);
    EmbedProperties CreateErrorEmbed(string description);
}