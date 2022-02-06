using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Modules;
using Newtonsoft.Json;
using NiTiS.Core.Additions;

namespace NiTiS.RPGBot;
public class RPGBot
{
    private readonly BotClient botClient;
    private readonly CommandService commandService;
    private readonly SaveModule saveModule;
    /// <summary>
    /// Color of Embed messages
    /// </summary>
    public Color CommandColor { get; protected set; }
    public SocketSelfUser Self => botClient.Self;
    public DiscordSocketClient Client => botClient.Client;

    public RPGBot(string token, Color? color = null)
    {
        color ??= Color.LightGrey;
        this.CommandColor = color.Value;
        this.botClient = new BotClient(token, "::");
        this.saveModule = new SaveModule(Directory.GetCurrentDirectory());
        commandService = new CommandService();
        commandService.AddModuleAsync<AdminModule>(null);
        commandService.AddModuleAsync<SuperUserModule>(null);
        botClient.CommandExecute += ExecuteCommand;

        SingletonManager.AddInstance(this);
        SingletonManager.AddInstance(saveModule);
    }
    public void Startup()
    {
        botClient.Startup();
        Client.SetGameAsync(botClient.Prefix + "help");
    }
    public Task ExecuteCommand(ICommandContext context, int argPos, SocketMessage? message)
    {
        Console.WriteLine($"[{message?.Author.Id}:{message?.Author}<{message?.Id}>]{message?.Content}");
        return commandService.ExecuteAsync(context, argPos, null);
    }
}