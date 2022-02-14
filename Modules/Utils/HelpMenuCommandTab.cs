using Discord;
using Discord.Commands;

namespace NiTiS.RPGBot.Modules.Utils;

public sealed class HelpMenuCommandTab
{
    private readonly string tabName;
    private readonly HelpMenuCommand[] commands;
    public HelpMenuCommandTab(string tabName, params string[] commands)
    {
        this.tabName = tabName;
        this.commands = commands.Select( (str, ind) =>
        {
            return new HelpMenuCommand(str);
        }).ToArray();
    }
    public string Name => tabName;
    public string NameKey => CommandHelper.GetCommandTabNameKey(tabName);
    public string DescriptionKey => CommandHelper.GetCommandTabDescriptionKey(tabName);
    public HelpMenuCommand[] Commands => commands;

    public Embed[] CreateEmbeds(SocketCommandContext context, BasicModule.RPGCommandContext rcontext)
    {
        Embed[] embeds = new Embed[commands.Length];
        for(int i = 0; i < commands.Length; i++)
        {
            HelpMenuCommand command = commands[i];
            embeds[i] = command.CreateEmbed(context, rcontext).WithBotColor().Build();
        }
        return embeds;
    }
}

public sealed class HelpMenuCommand
{
    private readonly string commandName;
    public HelpMenuCommand(string commandName)
    {
        this.commandName = commandName;
    }
    public string Name => commandName;
    public string NameKey => CommandHelper.GetCommandNameKey(commandName);
    public string DescriptionKey => CommandHelper.GetCommandDescriptionKey(commandName);
    public string UsageKey => CommandHelper.GetCommandUsageKey(commandName);
    public EmbedBuilder CreateEmbed(SocketCommandContext context, BasicModule.RPGCommandContext rcontext)
    {
        EmbedBuilder builder = new();
        builder.WithTitle(rcontext.GetTranslate(NameKey));
        builder.WithDescription(rcontext.GetTranslate(DescriptionKey));
        builder.AddField(rcontext.GetTranslate("cmd-usage"), rcontext.GetTranslate(UsageKey));
        return builder;
    }
}