using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public class BotModule : BasicModule
{
    [Command("bot")]
    [Summary("cmd.bot.description")]
    public async Task BotInfo()
    {
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        string T_botID = RPGContext.GetTranslate("cmd.help.bot-id");
        string T_botTags = RPGContext.GetTranslate("cmd.help.bot-tags");
        string T_version = RPGContext.GetTranslate("cmd.help.bot-version");
        string T_github = RPGContext.GetTranslate("cmd.help.bot-github");
        string T_botAuthor = RPGContext.GetTranslate("cmd.help.bot-author");
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotColor().WithBotAsAuthor();
        builder.AddField(T_botID, bot.Self.Id);
        builder.AddField(T_botTags, "`NiTiS` `RPG` `SOCIAL`");
        builder.AddField(T_version, RPGBot.Version);
        builder.AddField(T_github, "[`Link`](https://github.com/NiTiS-Dev/NiTiSRPGBot) [`Issues`](https://github.com/NiTiS-Dev/NiTiSRPGBot/issues)");
        builder.AddField(T_botAuthor, "<@!508012163307143168>");
        await ReplyAsync(embed: builder.Build());
    }
}