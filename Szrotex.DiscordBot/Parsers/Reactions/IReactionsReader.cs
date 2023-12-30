using NetCord.Rest;

namespace Szrotex.DiscordBot.Parsers.Reactions;

public interface IReactionsReader
{
    IEnumerable<ReactionEmojiProperties>? ReadFromString(string? toRead);
}