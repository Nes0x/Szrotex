namespace Szrotex.DiscordBot.Models;

#nullable disable
public class BeamngEvent
{
    public int id { get; init; }
    public string player { get; init; }
    public string value { get; init; }
    public string @event { get; init; }
    public string createdAt { get; init; }
}