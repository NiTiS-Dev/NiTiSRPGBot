using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.RPG;

public class HeroModule : BasicModule
{
    [Command("hero")]
    [Summary("cmd.hero.description")]
    public async Task HeroInfo(SocketUser user = null)
    {
        RPGGuild rguild = Context.Guild.ToRPGGuild();
        string T_error = RPGContext.GetTranslate("cmd.error");
        string T_noHero = RPGContext.GetTranslate("cmd.error.hero-doesnt-exists");
        user ??= Context.User;
        RPGUser ruser = user.ToRPGUser();

        if(ruser.Hero is null)
        {
            await ReplyError(T_error, T_noHero, Context.Message.Reference);
            return;
        }
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor().WithBotColor();
        ruser.Hero.AddFields(builder, rguild);
        await ReplyEmbed(builder);
    }
}