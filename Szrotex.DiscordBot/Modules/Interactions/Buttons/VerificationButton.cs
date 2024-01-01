using NetCord;
using NetCord.Rest;
using NetCord.Services.Interactions;
using Szrotex.DiscordBot.Config;
using Szrotex.DiscordBot.Embed;

namespace Szrotex.DiscordBot.Modules.Interactions.Buttons;

public class VerificationButton : InteractionModule<ButtonInteractionContext>
{
    private readonly EmbedCreator _embedCreator;
    private readonly BotConfig _config;


    public VerificationButton(EmbedCreator embedCreator, BotConfig config)
    {
        _embedCreator = embedCreator;
        _config = config;
    }

    [Interaction("verification")]
    public InteractionCallback Handle()
    {
        var guildUser = (GuildUser)Context.User;
        guildUser.AddRoleAsync(_config.Ids.VerificationRoleId);
        return InteractionCallback.Message(new InteractionMessageProperties()
            .AddEmbeds(_embedCreator.CreateEmbed(_config.MessagesConfig.SuccessTitle,
                _config.MessagesConfig.VerificationSucceed)).WithFlags(MessageFlags.Ephemeral));
        
    }
}