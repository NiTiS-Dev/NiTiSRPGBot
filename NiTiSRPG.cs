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

    public RPGBot(string dataDirectory, Color? color = null)
    {
        CommandColor = color ??= Color.LightGrey;
        
        saveModule = new SaveModule(dataDirectory);
        saveModule.InitializeDirectory();
        saveModule.LoadItems();
        saveModule.LoadLangs();

        botClient = new BotClient(saveModule.LoadToken(), "::");
        botClient.CommandExecute += ExecuteCommand;

        commandService = new CommandService();
        RegistryServices(commandService);
        
        SingletonManager.AddInstance(this);
        SingletonManager.AddInstance(commandService);
        SingletonManager.AddInstance(saveModule);
    }
    public virtual void RegistryServices(CommandService service)
    {
        service.AddModuleAsync<AdminModule>(null);
        service.AddModuleAsync<SuperUserModule>(null);
        service.AddModuleAsync<FightModule>(null);
        service.AddModuleAsync<BotModule>(null);
        service.AddModuleAsync<HelpModule>(null);
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