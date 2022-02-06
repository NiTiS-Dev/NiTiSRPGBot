using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Modules;

public class FightModule : ModuleBase<SocketCommandContext>
{
    public async Task Fight(IUser enemy = null)
    {
        if(enemy == null)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithBotAsAuthor();
            builder.WithBotColor();
            await ReplyAsync(null, false, builder.Build());
            return;
        }
    }
    public async Task Apply()
    {

    }
}