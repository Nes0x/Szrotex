using NetCord;
using NetCord.Rest;
using NetCord.Services.Interactions;
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Discord.Embed;

namespace Szrotex.DiscordBot.Discord.Modules.Interactions.Buttons;

public class VerificationButton : InteractionModule<ButtonInteractionContext>
{
    private readonly BotConfig _config;
    private readonly EmbedCreator _embedCreator;


    public VerificationButton(EmbedCreator embedCreator, BotConfig config)
    {
        _embedCreator = embedCreator;
        _config = config;
    }

    [Interaction("verification")]
    public InteractionCallback VerifyUser()
    {
        var guildUser = (GuildUser)Context.User;
        guildUser.AddRoleAsync(_config.Ids.VerificationRole);
        return InteractionCallback.Message(new InteractionMessageProperties()
            .AddEmbeds(_embedCreator.Create(_config.Messages.SuccessTitle,
                _config.Messages.VerificationSucceed)).WithFlags(MessageFlags.Ephemeral));
    }
}