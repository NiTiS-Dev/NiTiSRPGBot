using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace NiTiS.RPGBot.Modules.Administration;

public class ApplyXPModule : BasicModule
{
    [Command("apply-xp")]
    [Alias("ax")]
    [Summary("cmd.apply-xp.description")]
    public async Task ApplyXP(UInt64 amount = 0)
    {
        RPGGuild rguild = RPGContext.RGuild;
        string T_error = RPGContext.GetTranslate("cmd.error");
        string T_noHero = RPGContext.GetTranslate("cmd.error.hero-doesnt-exists");
        SocketUser user = Context.User;
        RPGUser ruser = user.ToRPGUser();

        if (ruser.Hero is null)
        {
            await ReplyError(T_error, T_noHero, Context.Message.Reference);
            return;
        }
        ruser.Hero.XPModule.ApplyXP(amount);
    }
}