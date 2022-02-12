using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public class UserGuildInfoModule : ModuleBase<SocketCommandContext>
{
    [Command("uinfo")]
    [Alias("user", "info", "about", "ufo", "ui", "uf")]
    [RequireContext(ContextType.Guild)]
    [Summary("cmd.user-info.description")]
    public async Task UserInfo(SocketUser user = null)
    {
        SaveModule saveModule = SingletonManager.GetInstance<SaveModule>();
        user ??= Context.User;
        RPGUser ruser = saveModule.LoadUser(user.Id);
        EmbedBuilder builder = new EmbedBuilder();

        builder.WithAuthor(user.Username, user.GetAvatarUrl());
        builder.WithBotColor();

        builder.AddField("id", user.Id);
        builder.AddField("admin", ruser.IsAdmin);
        //Items count
        if (Context.User.IsUserRPGAdmin())
            builder.AddField("Json", JsonConvert.SerializeObject(ruser, Formatting.Indented));
        await ReplyAsync(null, false, builder.Build());
    }
    [Command("ginfo")]
    [Alias("guild", "gifo", "gfo", "gf", "gi")]
    [RequireContext(ContextType.Guild)]
    [Summary("cmd.guild-info.description")]
    public async Task GuildInfo()
    {
        IGuild guild = Context.Guild;
        RPGGuild rguild = SingletonManager.GetInstance<SaveModule>().LoadGuild(guild.Id);
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithAuthor(guild.Name, guild.IconUrl);
        builder.WithBotColor();
        builder.AddField("Owner", guild.GetOwnerAsync().GetAwaiter().GetResult(), false);
        if (Context.User.IsUserRPGAdmin())
            builder.AddField("Json", JsonConvert.SerializeObject(rguild, Formatting.Indented));
        await ReplyAsync(null, false, builder.Build());
    }
}