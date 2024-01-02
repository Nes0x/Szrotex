using NetCord.Rest;
using NetCordAddons.Services.Modal;

namespace Szrotex.DiscordBot.Discord.ModalModels;

public class EmbedMessageModal : BaseModal
{
    protected override string CustomId { get; set; } = "embed";
    protected override string ModalTitle => "Stwórz embed";

    [ModalProperty(TextInputStyle.Short, Label = "Tytuł")]
    public string? Title { get; init; }

    [ModalProperty(TextInputStyle.Paragraph, Label = "Opis")]
    public string? Description { get; init; }

    [ModalProperty(TextInputStyle.Short, Label = "Kolor", Required = false)]
    public string? Color { get; init; }

    [ModalProperty(TextInputStyle.Paragraph, Label = "Reakcje",
        Placeholder = "Wzór: nazwaEmotki|reactionId,nazwaEmotki|reactionId2", Required = false)]
    public string? Reactions { get; init; }

    [ModalProperty(TextInputStyle.Paragraph, Label = "Przyciski",
        Placeholder = "Maksymalnie 5. Wzór: action|id|styl|nazwa|emojiId,link|url|nazwa|emojiId", Required = false)]
    public string? Buttons { get; init; }
}