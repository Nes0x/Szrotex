using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Hosting.Services.Interactions;
using NetCord.Services.ApplicationCommands;
using NetCord.Services.Interactions;
using Szrotex.DiscordBot;
using Szrotex.DiscordBot.Api;
using Szrotex.DiscordBot.Config;
using Szrotex.DiscordBot.Embed;
using Szrotex.DiscordBot.Parsers.Buttons;
using Szrotex.DiscordBot.Parsers.Reactions;
using Szrotex.DiscordBot.Timers;
using WebSocketSharp.Server;

var assembly = typeof(Program).Assembly;
var host = Host.CreateDefaultBuilder(args);
host
    .ConfigureServices(services => services.AddGatewayEventHandlers(assembly)
        .AddSingleton<ButtonsReader>()
        .AddSingleton<ReactionsReader>()
        .AddSingleton(_ = BotConfig.Create())
        .AddSingleton<EmbedCreator>()
        .AddSingleton<EmbedModifier>()
        .AddSingleton<OnlinePlayersTimer>()
        .AddTransient<HttpClient>()
        .AddTransient<ApiWrapper>(provider =>
        {
            var config = provider.GetRequiredService<BotConfig>();
            var httpClient = provider.GetRequiredService<HttpClient>();
            return new ApiWrapper(config.ApiUrl, httpClient);
        })
        .AddTransient<StatisticsDataProvider>())
    .UseDiscordGateway((options, provider) =>
    {
        var config = provider.GetRequiredService<BotConfig>();
        options.Token = args[0];
        options.Configuration = new GatewayClientConfiguration
        {
            Intents = GatewayIntents.All,
            ConnectionProperties = ConnectionPropertiesProperties.IOS,
            Presence = new PresenceProperties(UserStatusType.Online)
            {
                Activities = new[] { new UserActivityProperties(config.Status, UserActivityType.Streaming) }
            }
        };
    })
    .UseInteractionService<ModalSubmitInteraction, ModalSubmitInteractionContext>()
    .UseInteractionService<ButtonInteraction, ButtonInteractionContext>()
    .UseApplicationCommandService<SlashCommandInteraction, SlashCommandContext>();

var app = host.Build()
    .AddModules(assembly)
    .UseGatewayEventHandlers();

await app.RunAsync();