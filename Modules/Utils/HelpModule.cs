using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public class HelpModule : BasicModule
{
    [Obsolete]
    private static readonly string[] IGNORED_COMMANDS = new string[]
    {
        "accept",
    };

    public static readonly HelpMenuCommandTab[] TABS = new HelpMenuCommandTab[]
    {
        new("utils", "bot", "help"),
        new("user", "user-info", "hero", "hero-create", "hero-delete"),
        new("administration", "clear", "set-lang"),
    };

    private static volatile Dictionary<ulong, TabInstance> helpMenus = new();

    [Command("help")]
    [Alias("h", "commands")]
    public async Task Help(string? command = null, string? D_commandName = null)
    {
        try
        {
            if(command?.ToLower()?.Replace('_', '-') == "old-menu") 
            {
                CommandService service = SingletonManager.GetInstance<CommandService>();
                BotClient bot = SingletonManager.GetInstance<BotClient>();
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithBotAsAuthor().WithBotColor();
                if (D_commandName != null)
                {
                    foreach (CommandInfo cmd in service.Commands)
                    {
                        foreach (string alias in cmd.Aliases)
                        {
                            if (alias == D_commandName)
                            {
                                builder.WithTitle(bot.Prefix + cmd.Name);
                                builder.WithDescription(RPGContext.GetTranslate(cmd.Summary ?? "none"));
                                await ReplyEmbed(builder);
                                return;
                            }
                        }
                    }
                    await ReplyError(new CommandNotFoundException(D_commandName), RPGContext.Reference);
                    return;
                }
                foreach (var cmd in service.Commands)
                {
                    if (IGNORED_COMMANDS.Contains(cmd.Name))
                        continue;
                    builder.AddField(bot.Prefix + cmd.Name, RPGContext.GetTranslate(cmd.Summary ?? "none"), true);
                }
                await ReplyEmbed(builder, RPGContext.Reference);
                return;
            } //Obsolete menu
            if(command is not null)
            {
                CommandService service = SingletonManager.GetInstance<CommandService>();
                if (command != null)
                {
                    foreach (CommandInfo cmd in service.Commands)
                    {
                        foreach (string alias in cmd.Aliases)
                        {
                            if (alias == command)
                            {
                                await ReplyEmbed(new HelpMenuCommand(command).CreateEmbed(Context, RPGContext).WithBotAsAuthor().WithBotColor());
                                return;
                            }
                        }
                    }
                    await ReplyError(new CommandNotFoundException(command), RPGContext.Reference);
                    return;
                }
                return;
            }
            ComponentBuilder selectBuilder = new();
            SelectMenuBuilder selectMenuBuilder = new();
            selectMenuBuilder.CustomId = "help-tab-select";
            for(int i = 0; i < TABS.Length; i++)
            {
                HelpMenuCommandTab tab = TABS[i];
                selectMenuBuilder.AddOption(RPGContext.GetTranslate(tab.NameKey), i.ToString(), RPGContext.GetTranslate(tab.DescriptionKey), null, i == 0);
            }
            selectBuilder.WithSelectMenu(selectMenuBuilder);
            IUserMessage msg = await ReplyAsync(embeds: TABS[0].CreateEmbeds(Context, RPGContext), components: selectBuilder.Build());
            if(helpMenus.Count > 512)
            {
                helpMenus.Clear();
            }
            helpMenus.Add(msg.Id, new(msg, RPGContext, Context));
           
        }
        catch (Exception ex)
        {
            await ReplyError(ex.GetType().Name, ex.Message, RPGContext.Reference);
        }
    }
    public static async Task RewriteMenu(SocketMessageComponent component, byte menuID)
    {
        ulong msgID = component.Message.Id;
        if (helpMenus.ContainsKey(msgID))
        {
            var helpTab = helpMenus[msgID];
            await component.UpdateAsync((s) =>
            {
                ComponentBuilder selectBuilder = new();
                SelectMenuBuilder selectMenuBuilder = new();
                selectMenuBuilder.CustomId = "help-tab-select";
                for (int i = 0; i < TABS.Length; i++)
                {
                    HelpMenuCommandTab tab = TABS[i];
                    selectMenuBuilder.AddOption(helpTab.RPGContext.GetTranslate(tab.NameKey), i.ToString(), helpTab.RPGContext.GetTranslate(tab.DescriptionKey), null, i == menuID);
                }
                selectBuilder.WithSelectMenu(selectMenuBuilder);
                s.Embeds = TABS[menuID].CreateEmbeds(helpTab.Context, helpTab.RPGContext);
                s.Components = selectBuilder.Build();
            });
            return;
        }
    }
    private sealed class TabInstance
    {
        public IUserMessage Message { get; private set; }
        public RPGCommandContext RPGContext { get; private set; }
        public SocketCommandContext Context { get; private set; }
        public TabInstance(IUserMessage message, RPGCommandContext rcontext, SocketCommandContext context)
        {
            this.Message = message;
            this.RPGContext = rcontext;
            this.Context = context;
        }
    }
}

public sealed class CommandNotFoundException : Exception
{
    public CommandNotFoundException(string commandName) : base($"Command `{commandName}` not found")
    {

    }
}