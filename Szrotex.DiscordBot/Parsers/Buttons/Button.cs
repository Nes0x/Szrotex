namespace Szrotex.DiscordBot.Parsers.Buttons;

public class Button
{
    public required Type ButtonType { get; init; }
    public required object ButtonAsObject { get; init; }
}