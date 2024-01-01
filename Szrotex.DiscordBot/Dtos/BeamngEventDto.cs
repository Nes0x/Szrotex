using Szrotex.DiscordBot.Models;

namespace Szrotex.DiscordBot.Dtos;

#nullable disable
public class BeamngEventDto
{
    public string Player { get; init; }
    public string Value { get; init; }
    public string Event { get; init; }
    
    
    public static explicit operator BeamngEventDto(BeamngEvent beamngEvent)
    {
        return new BeamngEventDto
        {
            Player = beamngEvent.player,
            Value = beamngEvent.value,
            Event = beamngEvent.@event
        };
    }
}