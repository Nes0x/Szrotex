namespace Szrotex.DiscordBot;

#nullable disable
public class BeamngEventDto
{
    public string Player { get; init; }
    public string Value { get; init; }
    
    
    public static implicit operator BeamngEventDto(BeamngEvent beamngEvent)
    {
        return new BeamngEventDto
        {
            Player = beamngEvent.player,
            Value = beamngEvent.value
        };
    }
}