using System.Text;

namespace Szrotex.DiscordBot.Extensions;

public static class StringExtensions
{
    public static string BuildStringFromWords(this IEnumerable<string> words)
    {
        var stringBuilder = new StringBuilder();
        foreach (var word in words) stringBuilder.Append(word).Append(" ");

        return stringBuilder.ToString();
    }
}