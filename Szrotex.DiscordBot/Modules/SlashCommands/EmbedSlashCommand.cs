using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using Szrotex.DiscordBot.ModalModels;

namespace Szrotex.DiscordBot.Modules.SlashCommands;

public class EmbedSlashCommand : ApplicationCommandModule<SlashCommandContext>
{

    [SlashCommand("embed", "Stwórz embed.", DMPermission = false,
        DefaultGuildUserPermissions = Permissions.Administrator)]
    public InteractionCallback Handle(
        [SlashCommandParameter(Name = "kanał", Description = "Podaj kanał.",
            AllowedChannelTypes = new[] { ChannelType.TextGuildChannel, ChannelType.AnnouncementGuildChannel })]
        TextChannel textChannel,
        [SlashCommandParameter(Name = "obrazek", Description = "Podaj link do obrazka.")]
        string image = "",
        [SlashCommandParameter(Name = "stopka", Description = "Podaj link do stopki.")]
        string thumbnailImage = "")
    {
        var embed = new EmbedMessageModal();
        embed.AddParameterToId(textChannel.Id, image, thumbnailImage);
        return InteractionCallback.Modal(embed.ToModalProperties());
    }
}