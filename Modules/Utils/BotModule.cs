using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Modules.Utils;

public class BotModule : ModuleBase<SocketCommandContext>
{
    [Command("bot")]
    public async Task BotInfo()
    {
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotColor().WithBotAsAuthor();
        builder.AddField("Bot ID", bot.Self.Id);
        builder.AddField("Bot Tags", "`NiTiS` `RPG` `SOCIAL`");
        builder.AddField("Version", RPGBot.Version);
        builder.AddField("Bot Author", "<@!508012163307143168>");
        await ReplyAsync(embed: builder.Build());
    }
}