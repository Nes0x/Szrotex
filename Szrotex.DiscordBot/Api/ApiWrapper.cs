namespace Szrotex.DiscordBot.Api;

public class ApiWrapper
{
    private readonly HttpClient _httpClient;

    public ApiWrapper(string websiteUrl, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(websiteUrl);
    }

    public async Task<string> GetMethodAsync(string path)
    {
        var response = await _httpClient.GetAsync(path);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task PostMethodAsync(string path, HttpContent httpContent)
    {
        var response = await _httpClient.PostAsync(path, httpContent);
        response.EnsureSuccessStatusCode();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}