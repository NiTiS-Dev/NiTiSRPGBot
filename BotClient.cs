using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Modules;
using Newtonsoft.Json;
using NiTiS.Core.Additions;

namespace NiTiS.RPGBot;
public delegate Task CommandExecute(ICommandContext context, int argPos, SocketMessage? message);
public class BotClient
{
    private readonly DiscordSocketClient client;
    private readonly string token;
    public string Prefix { get; set; }
    public SocketSelfUser Self => client.CurrentUser;
    public DiscordSocketClient Client => client;

    public event CommandExecute CommandExecute;

    public BotClient(string token, string botPrefix = "::")
    {
        this.token = token;
        this.Prefix = botPrefix;
        client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
            LogLevel = LogSeverity.Info,
            AlwaysDownloadUsers = true,

        });
        client.Log += Log;
        client.Ready += LogReady;
        client.MessageReceived += MessageReceived;
        client.InteractionCreated += InteractionCreated;

        SingletonManager.AddInstance(this);
    }
    public void Startup(UserStatus userStatus = UserStatus.Online)
    {
        client.LoginAsync(TokenType.Bot, token).Wait();
        client.StartAsync().Wait();
        client.SetStatusAsync(userStatus).Wait();
    }
    protected virtual Task Log(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }
    protected async virtual Task LogReady()
    {
        Console.WriteLine($"{client.CurrentUser} is connected!");
    }
    protected virtual Task MessageReceived(SocketMessage message)
    {

        if (!(message is SocketUserMessage userMessage))
            return Task.CompletedTask;
        if (userMessage.Source != MessageSource.User)
            return Task.CompletedTask;

        var argPos = 0;

        if (!userMessage.HasStringPrefix(Prefix, ref argPos))
            if (!userMessage.HasMentionPrefix(Self, ref argPos))
                return Task.CompletedTask;

        var context = new SocketCommandContext(client, userMessage);

        CommandExecute?.Invoke(context, argPos, message);

        return Task.CompletedTask;
    }
    protected async virtual Task InteractionCreated(SocketInteraction interaction)
    {
        if (interaction is SocketMessageComponent component)
        {
            if (component.Data.CustomId == "unique-id")
                await interaction.RespondAsync("Thank you for clicking my button!");
            else Console.WriteLine("An ID has been received that has no handler!");
        }
    }
}