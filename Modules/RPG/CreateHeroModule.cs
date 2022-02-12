using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.RPG;

public class CreateHeroModule : ModuleBase<SocketCommandContext>
{
    [Command("create-hero")]
    [Alias("hero-create")]
    [Summary("cmd.create-hero.description")]
    public async Task CrateHero()
    {
        RPGGuild rguild = Context.Guild.ToRPGGuild();
        string T_allreadyExists = rguild.GetTranslate("cmd.error.hero-allready-exists");
        string T_heroCreated = rguild.GetTranslate("cmd.create-hero.AAAAAAAAA");
        SocketUser user = Context.User;
        RPGUser ruser = user.ToRPGUser();

        if(ruser.Hero is null)
        {
            ruser.Hero = new RPGHero()
            {

            };
        }else
        {
            await ReplyAsync();
        }
    }
}