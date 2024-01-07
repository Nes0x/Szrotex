using NetCord.Gateway;
using NetCord.Rest;
using Szrotex.DiscordBot.Discord.Embed;
using Szrotex.DiscordBot.Factories;
using WebSocketSharp;

namespace Szrotex.DiscordBot.Handlers.Wss;

public class BeamngChatWss : WssHandler
{
    private readonly BeamngEventDtoFactory _beamngEventDtoFactory;
    private readonly GatewayClient _client;
    private readonly EmbedCreator _embedCreator;

    public BeamngChatWss(EmbedCreator embedCreator, GatewayClient client, BeamngEventDtoFactory beamngEventDtoFactory)
    {
        _embedCreator = embedCreator;
        _client = client;
        _beamngEventDtoFactory = beamngEventDtoFactory;
    }

    protected override void OnMessage(object? sender, MessageEventArgs args)
    {
        var beamngEventDto = _beamngEventDtoFactory.CreateFromJson(args.Data);
        if (beamngEventDto == null) return;
        _client.Rest.SendMessageAsync(beamngEventDto.ChannelId,
            new MessageProperties().WithEmbeds(new[]
                { _embedCreator.Create(beamngEventDto.Title, beamngEventDto.Message) }));
    }
}