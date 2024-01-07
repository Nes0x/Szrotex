using NetCord;
using NetCord.Rest;
using NetCord.Services.Interactions;
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Discord.Embed;
using Szrotex.DiscordBot.Discord.ModalModels;
using Szrotex.DiscordBot.Discord.Parsers.Buttons;
using Szrotex.DiscordBot.Discord.Parsers.Reactions;

namespace Szrotex.DiscordBot.Discord.Modules.Interactions.Modals;

public class EmbedModal : InteractionModule<ModalSubmitInteractionContext>
{
    private readonly ButtonsReader _buttonsReader;
    private readonly BotConfig _config;
    private readonly EmbedCreator _embedCreator;
    private readonly ReactionsReader _reactionsReader;

    public EmbedModal(EmbedCreator embedCreator, BotConfig config, ButtonsReader buttonsReader,
        ReactionsReader reactionsReader)
    {
        _embedCreator = embedCreator;
        _config = config;
        _buttonsReader = buttonsReader;
        _reactionsReader = reactionsReader;
    }

    [Interaction("embed")]
    public async Task<InteractionCallback> HandleAsync(ulong id, string image, string thumbnailImage)
    {
        var embed = new EmbedMessageModal();
        embed.Load(Context.Components);
        var messageProperties = new MessageProperties().AddEmbeds(_embedCreator
            .Create(embed.Title!, embed.Description!, embed.Color).WithImage(image).WithThumbnail(thumbnailImage)
        );
        ActionRowProperties? buttons = _buttonsReader.ReadFromString(embed.Buttons);
        if (buttons is not null) messageProperties.AddComponents(buttons);
        var message = await ((TextChannel)Context.Guild!.Channels[id]).SendMessageAsync(
            messageProperties);
        IEnumerable<ReactionEmojiProperties>? reactions = _reactionsReader.ReadFromString(embed.Reactions);
        if (reactions is not null)
            foreach (var reactionEmojiProperties in reactions)
                await message.AddReactionAsync(reactionEmojiProperties);
        return InteractionCallback.Message(new InteractionMessageProperties()
            .AddEmbeds(_embedCreator.Create(_config.MessagesConfig.SuccessTitle,
                _config.MessagesConfig.EmbedCreated)).WithFlags(MessageFlags.Ephemeral));
    }
}