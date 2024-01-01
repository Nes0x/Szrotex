using NetCord.Gateway;
using NetCord.Rest;

namespace Szrotex.DiscordBot.Discord.Embed;

public class EmbedModifier
{
    private readonly GatewayClient _client;

    public EmbedModifier(GatewayClient client)
    {
        _client = client;
    }

    public async Task ModifyEmbedAsync(ulong channelId, ulong messageId, EmbedProperties toModify)
    {
        await _client.Rest.ModifyMessageAsync(
            channelId,
            messageId,
            options => options.WithEmbeds(new [] {toModify}));

    }
}