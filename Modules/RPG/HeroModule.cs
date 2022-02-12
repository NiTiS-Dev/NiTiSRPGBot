using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.RPG;

public class HeroModule : ModuleBase<SocketCommandContext>
{
    [Command("hero")]
    [Summary("cmd.hero.description")]
    public async Task HeroInfo(SocketUser user = null)
    {
        RPGGuild rguild = Context.Guild.ToRPGGuild();
        string T_allreadyExists = rguild.GetTranslate("cmd.error.hero-doesnt-exists");
        user ??= Context.User;
        RPGUser ruser = user.ToRPGUser();

        if(ruser.Hero is null)
        {
            //Send error to chat
            return;
        }
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor().WithBotColor();
        ruser.Hero.AddFields(builder);
        await ReplyAsync(embed: builder.Build());
    }
}