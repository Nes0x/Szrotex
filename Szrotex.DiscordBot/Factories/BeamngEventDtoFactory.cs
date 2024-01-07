using System.Text.Json;
using System.Web;
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
        var messages = _config.Messages;
        switch (beamngEvent.@event)
        {
            case "CHAT":
                return CreateFromChatEvent(beamngEvent);
            case "EVENT":
                switch (beamngEvent.value)
                {
                    case "onPlayerJoin":
                        return CreateFromServerEvent(beamngEvent, messages.PlayerJoinOnServer);
                    case "onPlayerDisconnect":
                        return CreateFromServerEvent(beamngEvent,messages.PlayerDisconnectFromServer);
                    case "onVehicleReset":
                        return CreateFromServerEvent(beamngEvent, messages.VehicleReset);
                    case "onVehicleSpawn": 
                        return CreateFromServerEvent(beamngEvent, messages.VehicleSpawn);
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    private BeamngEventDto CreateFromServerEvent(BeamngEvent beamngEvent, string message)
    {
        var beamngEventDto = new BeamngEventDto(_config.Messages.EventEmbed, string.Format(message, beamngEvent.player), _config.Ids.BeamngEventsChannelId);
        return beamngEventDto;
    }

    private BeamngEventDto CreateFromChatEvent(BeamngEvent beamngEvent)
    {
        var decodedData = HttpUtility.HtmlDecode(beamngEvent.value);
        List<string> messageWords = decodedData.Split(" ").ToList();
        var correctWords = RemoveUnusedWords(messageWords); 
        var beamngEventDto = new BeamngEventDto($"{beamngEvent.player}", correctWords.BuildStringFromWords(), _config.Ids.BeamngChatChannelId);
        return beamngEventDto;
    }


    private static IEnumerable<string> RemoveUnusedWords(IEnumerable<string> words)
    {
        var result = words.ToList();
        result.RemoveRange(0, 3);
        return result;
    }
    
    
    
    
}
