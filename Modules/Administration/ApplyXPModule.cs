using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace NiTiS.RPGBot.Modules.Administration;

public class ApplyXPModule : ModuleBase<SocketCommandContext>
{
    [Command("apply-xp")]
    [Alias("ax")]
    [Summary("cmd.apply-xp.description")]
    public async Task ApplyXP(UInt64 amount = 0)
    {
        RPGGuild rguild = Context.Guild.ToRPGGuild();
        string T_allreadyExists = rguild.GetTranslate("cmd.error.hero-doesnt-exists");
        SocketUser user = Context.User;
        RPGUser ruser = user.ToRPGUser();

        if (ruser.Hero is null)
        {
            //Send error to chat
            return;
        }
        ruser.Hero.XPModule.ApplyXP(amount);
    }
}