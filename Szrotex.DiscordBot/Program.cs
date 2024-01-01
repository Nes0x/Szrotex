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
using Szrotex.DiscordBot.Discord.Config;
using Szrotex.DiscordBot.Discord.Embed;
using Szrotex.DiscordBot.Discord.Parsers.Buttons;
using Szrotex.DiscordBot.Discord.Parsers.Reactions;
using Szrotex.DiscordBot.Factories;
using Szrotex.DiscordBot.Handlers.Timers;
using Szrotex.DiscordBot.Handlers.Wss;

if (args.Length != 2)
    throw new ArgumentException("You must provide two arguments, first - token and second - file name for config.");

var assembly = typeof(Program).Assembly;
var host = Host.CreateDefaultBuilder(args);
host
    .ConfigureServices(services => services.AddGatewayEventHandlers(assembly)
        .AddSingleton<ButtonsReader>()
        .AddSingleton<ReactionsReader>()
        .AddSingleton(_ = BotConfig.Create(args[1]))
        .AddSingleton<EmbedCreator>()
        .AddSingleton<EmbedModifier>()
        .AddSingleton<OnlinePlayersTimer>()
        .AddSingleton<BeamngChatWss>()
        .AddSingleton<BeamngEventDtoFactory>()
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