namespace Szrotex.DiscordBot.Dtos;

#nullable disable
public class BeamngEventDto
{
    public BeamngEventDto(string title, string message, ulong channelId)
    {
        Title = title;
        Message = message;
        ChannelId = channelId;
    }

    public string Title { get; init; }
    public string Message { get; init; }
    public ulong ChannelId { get; init; }
}