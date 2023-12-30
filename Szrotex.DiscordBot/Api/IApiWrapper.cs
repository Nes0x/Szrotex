namespace Szrotex.DiscordBot.Api;

public interface IApiWrapper : IDisposable
{
    Task<string> GetMethodAsync(string path);
    Task PostMethodAsync(string path, HttpContent httpContent);
}