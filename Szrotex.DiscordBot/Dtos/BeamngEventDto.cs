using Szrotex.DiscordBot.Models;

namespace Szrotex.DiscordBot.Dtos;

#nullable disable
public class BeamngEventDto
{
    public string Title { get; init; }
    public string Message { get; init; }

    public BeamngEventDto(string title, string message)
    {
        Title = title;
        Message = message;
    }
}