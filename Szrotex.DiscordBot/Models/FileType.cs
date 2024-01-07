namespace Szrotex.DiscordBot.Models;

public readonly struct FileType
{
    public string Name { get; }
    public string Extension { get; }

    public FileType(string name, string extension)
    {
        Name = name;
        Extension = extension;
    }

    public string AsPath() => $"{Name}.{Extension}";
}