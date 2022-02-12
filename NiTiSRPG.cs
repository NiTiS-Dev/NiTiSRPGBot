﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Modules.RPG;
using NiTiS.RPGBot.Modules.Administration;
using NiTiS.RPGBot.Modules.Utils;
using NiTiS.RPGBot.Modules;

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
    public Color ErrorColor { get; protected set; }
    public SocketSelfUser Self => botClient.Self;
    public DiscordSocketClient Client => botClient.Client;
    public static Version Version => new Version(0,6,1);

    public RPGBot(string dataDirectory, Color? color = null, Color? errorColor = null)
    {
        CommandColor = color ??= Color.LightGrey;
        ErrorColor = errorColor ??= new Color(235, 104, 77);
        
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
        //Admin commands
        service.AddModuleAsync<BotModule>(null);
        service.AddModuleAsync<HelpModule>(null);
        service.AddModuleAsync<ClearModule>(null);
        service.AddModuleAsync<RenameModule>(null);
        service.AddModuleAsync<SetLanguageModule>(null);
        service.AddModuleAsync<ApplyXPModule>(null);
        //Utils
        service.AddModuleAsync<UserGuildInfoModule>(null);
        service.AddModuleAsync<UptimeModule>(null);
        //RPG
        service.AddModuleAsync<FightModule>(null);
        service.AddModuleAsync<CreateHeroModule>(null);
        service.AddModuleAsync<HeroModule>(null);

        //Others
        service.AddModuleAsync<SuperUserModule>(null);
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