using NetCord;
using NetCord.Rest;

namespace Szrotex.DiscordBot.Discord.Parsers.Buttons;

public class ButtonsReader
{
    public ActionRowProperties? ReadFromString(string? toRead)
    {
        if (string.IsNullOrWhiteSpace(toRead)) return null;
        var buttons = new List<Button>();
        var encodedButtons = toRead.Split(",");
        var encodedButtonAttributes = encodedButtons.Select(button => button.Split("|")).ToList();

        encodedButtonAttributes.ForEach(buttonAttributes =>
        {
            if (buttons.Count >= 5) return;
            var button = ReadButton(buttonAttributes.ToList(), out var type);
            if (button is not null)
                buttons.Add(new Button
                {
                    ButtonType = type!,
                    ButtonAsObject = button
                });
        });

        if (!buttons.Any()) return null;

        var actionRowProperties = new ActionRowProperties(new List<ButtonProperties>());

        foreach (var button in buttons)
        {
            var buttonAsObject = button.ButtonAsObject;
            if (button.ButtonType == typeof(ActionButtonProperties))
                actionRowProperties.AddButtons((ActionButtonProperties)buttonAsObject);
            else
                actionRowProperties.AddButtons((LinkButtonProperties)buttonAsObject);
        }

        return actionRowProperties;
    }

    private object? ReadButton(List<string> buttonAttributes, out Type? type)
    {
        var buttonType = buttonAttributes[0].ToLower();
        buttonAttributes.RemoveAt(0);
        switch (buttonType)
        {
            case "action":
                type = typeof(ActionButtonProperties);
                return ReadActionButton(new ActionButton
                {
                    Id = buttonAttributes[0],
                    ButtonStyle = Enum.Parse<ButtonStyle>(buttonAttributes[1]),
                    Name = buttonAttributes[2],
                    EmojiId = buttonAttributes[3]
                });
            case "link":
                type = typeof(LinkButtonProperties);
                return ReadLinkButton(new LinkButton
                {
                    Url = buttonAttributes[0],
                    Name = buttonAttributes[1],
                    EmojiId = buttonAttributes[2]
                });
            default:
                type = null;
                return null;
        }
    }


    private ActionButtonProperties? ReadActionButton(ActionButton actionButton)
    {
        if (actionButton.Id is null || actionButton.ButtonStyle is null) return null;

        var buttonStyle = actionButton.ButtonStyle.Value;

        if (!string.IsNullOrWhiteSpace(actionButton.Name) && !string.IsNullOrWhiteSpace(actionButton.EmojiId))
            return new ActionButtonProperties(actionButton.Id, actionButton.Name,
                new EmojiProperties(ulong.Parse(actionButton.EmojiId)), buttonStyle);

        if (!string.IsNullOrWhiteSpace(actionButton.Name))
            return new ActionButtonProperties(actionButton.Id, actionButton.Name, buttonStyle);

        if (!string.IsNullOrWhiteSpace(actionButton.EmojiId))
            return new ActionButtonProperties(actionButton.Id, new EmojiProperties(ulong.Parse(actionButton.EmojiId)),
                buttonStyle);

        return null;
    }

    private LinkButtonProperties? ReadLinkButton(LinkButton linkButton)
    {
        if (string.IsNullOrWhiteSpace(linkButton.Url)) return null;

        if (!string.IsNullOrWhiteSpace(linkButton.Name) && !string.IsNullOrWhiteSpace(linkButton.EmojiId))
            return new LinkButtonProperties(linkButton.Url, linkButton.Name,
                new EmojiProperties(ulong.Parse(linkButton.EmojiId)));

        if (!string.IsNullOrWhiteSpace(linkButton.Name))
            return new LinkButtonProperties(linkButton.Url, linkButton.Name);

        if (!string.IsNullOrWhiteSpace(linkButton.EmojiId))
            return new LinkButtonProperties(linkButton.Url, new EmojiProperties(ulong.Parse(linkButton.EmojiId)));

        return null;
    }
}