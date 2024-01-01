using NetCord;

namespace Szrotex.DiscordBot.Discord.Parsers.Buttons;

public class ActionButton
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public ButtonStyle? ButtonStyle { get; init; }
    public string? EmojiId { get; init; }
}