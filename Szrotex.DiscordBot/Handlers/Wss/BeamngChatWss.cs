using System.Text.Json;
using NetCord.Gateway;
using NetCord.Rest;
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Discord.Embed;
using Szrotex.DiscordBot.Dtos;
using Szrotex.DiscordBot.Extensions;
using Szrotex.DiscordBot.Models;
using WebSocketSharp;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace Szrotex.DiscordBot.Handlers.Wss;

public class BeamngChatWss : WssHandler
{
    private readonly EmbedCreator _embedCreator;
    private readonly BotConfig _botConfig;
    private readonly GatewayClient _client;

    public BeamngChatWss(EmbedCreator embedCreator, BotConfig botConfig, GatewayClient client)
    {
        _embedCreator = embedCreator;
        _botConfig = botConfig;
        _client = client;
    }

    protected override void OnError(object? sender, ErrorEventArgs args)
    {
    }

    protected override void OnMessage(object? sender, MessageEventArgs args)
    {
        var beamngEventDto = (BeamngEventDto)JsonSerializer.Deserialize<BeamngEvent>(args.Data);

        if (beamngEventDto.Event != "CHAT") return;


        List<string> messageWords = beamngEventDto.Value.Split(" ").ToList();
        messageWords.RemoveAt(0);
        
        _client.Rest.SendMessageAsync(_botConfig.Ids.BeamngChatChannelId,
            new MessageProperties().WithEmbeds(new[] { _embedCreator.CreateEmbed(beamngEventDto.Player, messageWords.BuildStringFromWords()) }));
    }



}