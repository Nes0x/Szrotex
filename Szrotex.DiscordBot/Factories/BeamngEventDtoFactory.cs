using System.Text.Json;
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Dtos;
using Szrotex.DiscordBot.Extensions;
using Szrotex.DiscordBot.Models;

namespace Szrotex.DiscordBot.Factories;

public class BeamngEventDtoFactory
{
    private readonly BotConfig _config;

    public BeamngEventDtoFactory(BotConfig config)
    {
        _config = config;
    }


    public BeamngEventDto? CreateFromJson(string json)
    {
        var beamngEvent = JsonSerializer.Deserialize<BeamngEvent>(json);
        if (beamngEvent is null) throw new ArgumentNullException(nameof(json), "Typed argument is not correct.");

        switch (beamngEvent.@event)
        {
            case "CHAT":
                return CreateFromChatEvent(beamngEvent);
            case "EVENT":
                switch (beamngEvent.value)
                {
                    case "onPlayerJoin":
                        return CreateFromPlayerServerEvent(beamngEvent, "Gracz {0} wszedł na serwer.");
                    case "onPlayerDisconnect":
                        return CreateFromPlayerServerEvent(beamngEvent, "Gracz {0} wyszedł z serwera.");
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    private BeamngEventDto CreateFromPlayerServerEvent(BeamngEvent beamngEvent, string message)
    {
        var beamngEventDto = new BeamngEventDto("Zdarzenie", string.Format(message, beamngEvent.player),
            _config.Ids.BeamngEventsChannelId);
        return beamngEventDto;
    }

    private BeamngEventDto CreateFromChatEvent(BeamngEvent beamngEvent)
    {
        var messageWords = beamngEvent.value.Split(" ").ToList();
        messageWords.RemoveAt(0);
        var beamngEventDto = new BeamngEventDto($"{beamngEvent.player}", messageWords.BuildStringFromWords(),
            _config.Ids.BeamngChatChannelId);
        return beamngEventDto;
    }
}