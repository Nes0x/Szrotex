using NetCord.Rest;

namespace Szrotex.DiscordBot.Parsers.Buttons;

public interface IButtonsReader
{
    ActionRowProperties? ReadFromString(string? toRead);
}