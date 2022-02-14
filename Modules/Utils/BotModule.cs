using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;
using System.Reflection;

namespace NiTiS.RPGBot.Modules.Utils;

public class BotModule : BasicModule
{
    public static readonly Lazy<string> DotNetVersion = new( () =>
    {
        var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
        var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
        int netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
        if (netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2)
            return assemblyPath[netCoreAppIndex + 1];
        return null;
    });
    [Command("bot")]
    [Summary("cmd.bot.description")]
    public async Task BotInfo()
    {
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        string T_botID = RPGContext.GetTranslate("cmd.help.bot-id");
        string T_botTags = RPGContext.GetTranslate("cmd.help.bot-tags");
        string T_version = RPGContext.GetTranslate("cmd.help.bot-version");
        string T_dotNetVersion = RPGContext.GetTranslate("cmd.help.dotnet-version");
        string T_github = RPGContext.GetTranslate("cmd.help.bot-github");
        string T_botAuthor = RPGContext.GetTranslate("cmd.help.bot-author");
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotColor().WithBotAsAuthor();
        builder.AddField(T_botID, bot.Self.Id);
        builder.AddField(T_botTags, "`NiTiS` `RPG` `SOCIAL`");
        builder.AddField(T_version, RPGBot.Version);
        builder.AddField(T_dotNetVersion, DotNetVersion.Value ?? RPGContext.GetTranslate("none"));
        builder.AddField(T_github, "[`Link`](https://github.com/NiTiS-Dev/NiTiSRPGBot) [`Issues`](https://github.com/NiTiS-Dev/NiTiSRPGBot/issues)");
        builder.AddField(T_botAuthor, "<@!508012163307143168>");
        await ReplyAsync(embed: builder.Build());
    }
}