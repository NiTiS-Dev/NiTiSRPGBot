using Discord;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public sealed class HelpMenuCommandTab
{
    private readonly string tabName;
    private readonly HelpMenuCommand[] commands;
    public HelpMenuCommandTab(string tabName, params string[] commands)
    {
        this.tabName = tabName;
        this.commands = commands.Select((str, ind) =>
       {
           return new HelpMenuCommand(str);
       }).ToArray();
    }
    public string Name => tabName;
    public string NameKey => CommandHelper.GetCommandTabNameKey(tabName);
    public string DescriptionKey => CommandHelper.GetCommandTabDescriptionKey(tabName);
    public HelpMenuCommand[] Commands => commands;

    public Embed[] CreateEmbeds(BasicModule.RPGCommandContext rcontext) => CreateEmbeds(Language.GetLanguage(rcontext.RGuild.Lang));
    public Embed[] CreateEmbeds(Language lang)
    {
        Embed[] embeds = new Embed[commands.Length];
        for (int i = 0; i < commands.Length; i++)
        {
            HelpMenuCommand command = commands[i];
            embeds[i] = command.CreateEmbed(lang).WithBotColor().Build();
        }
        return embeds;
    }
}

public sealed class HelpMenuCommand
{
    private readonly string commandName;
    private readonly BotClient bot;
    public HelpMenuCommand(string commandName)
    {
        this.bot = SingletonManager.GetInstance<BotClient>();
        this.commandName = commandName;
    }
    public string Name => commandName;
    public string NameKey => CommandHelper.GetCommandNameKey(commandName);
    public string DescriptionKey => CommandHelper.GetCommandDescriptionKey(commandName);
    public string UsageKey => CommandHelper.GetCommandUsageKey(commandName);
    public EmbedBuilder CreateEmbed(BasicModule.RPGCommandContext rcontext) => CreateEmbed(Language.GetLanguage(rcontext.RGuild.Lang));
    public EmbedBuilder CreateEmbed(Language lang)
    {
        EmbedBuilder builder = new();
        builder.WithTitle(bot.Prefix + Name);
        builder.WithDescription(lang.GetValue(DescriptionKey));
        builder.AddField(lang.GetValue("cmd-usage"), lang.GetValue(UsageKey));
        return builder;
    }
}