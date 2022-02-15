using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Modules.RPG;
using NiTiS.RPGBot.Modules.Utils;

namespace NiTiS.RPGBot;

public class BotClient
{
    private readonly DiscordSocketClient client;
    private readonly string token;
    public string Prefix { get; set; }
    public SocketSelfUser Self => client.CurrentUser;
    public DiscordSocketClient Client => client;

    public event Func<ICommandContext, int, SocketMessage, Task>? CommandExecute;
    public event Func<SocketMessageComponent, Task>? SelectMenuSelected;

    public BotClient(string? token = null, string botPrefix = "::")
    {
        ArgumentNullException.ThrowIfNull(token, nameof(token));
        this.token = token;
        this.Prefix = botPrefix;
        client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
            LogLevel = LogSeverity.Info,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
        });
        client.Log += Log;
        client.Ready += LogReady;
        client.MessageReceived += MessageReceived;
        client.SelectMenuExecuted += SelectMenuExecuted;
        client.InteractionCreated += InteractionCreated;
        client.ModalSubmitted += ModalSubmitted;
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
    protected virtual async Task InteractionCreated(SocketInteraction interaction)
    {
        if (interaction is SocketMessageComponent component)
        {
            if (component.Data.CustomId == "ignore")
                await interaction.DeferAsync();
            if (component.Data.CustomId == "delete-hero")
                await RebootHeroModule.ButtonDeleteClicked(component);
            else if (component.Data.CustomId == "delete-self")
                await RebootHeroModule.ButtonCancelClicked(component);
            else Console.WriteLine($"An ID has been received that has no handler! ID: {component.Data.CustomId}");
        }
    }
    protected virtual async Task SelectMenuExecuted(SocketMessageComponent arg)
    {
        SelectMenuSelected?.Invoke(arg);
        if (arg.Data.CustomId == "help-tab-select")
        {
            try
            {
                string[] args = arg.Data.Values.First().Split(':', StringSplitOptions.RemoveEmptyEntries);
                if (byte.TryParse(args[1], out var value))
                {
                    await HelpModule.RewriteMenu(arg, Language.GetLanguage(args[0]), value);
                }
            }
            catch (Exception)
            {
                await arg.Message.DeleteAsync();
            }
        }
        
    }
    private async Task ModalSubmitted(SocketModal arg)
    {
        if (arg.Data.CustomId.StartsWith("ignore"))
        {
            await arg.DeferAsync();
            return;
        }
    }
}