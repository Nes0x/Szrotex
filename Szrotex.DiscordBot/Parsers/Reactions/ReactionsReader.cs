using NetCord.Rest;

namespace Szrotex.DiscordBot.Parsers.Reactions;

public class ReactionsReader
{
    public IEnumerable<ReactionEmojiProperties>? ReadFromString(string? toRead)
    {
        if (string.IsNullOrWhiteSpace(toRead)) return null;
        var encodedReactions = toRead.Split(",");
        return encodedReactions.Select(encodedReaction => encodedReaction.Split("|")).Select(encodedReaction =>
            new ReactionEmojiProperties(encodedReaction[0], ulong.Parse(encodedReaction[1])));
    }
}