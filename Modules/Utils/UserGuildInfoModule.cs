using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NiTiS.RPGBot.Modules.Utils;

public class UserGuildInfoModule : BasicModule
{
    [Command("user-info")]
    [Alias("user", "info", "about", "ufo", "ui", "uf", "uinfo", "userinfo")]
    [RequireContext(ContextType.Guild)]
    [Summary("cmd.user-info.description")]
    public async Task UserInfo(SocketUser user = null)
    {
        user ??= Context.User;
        RPGUser ruser = RPGContext.RUser;
        EmbedBuilder builder = new();

        builder.WithAuthor(user.Username, user.GetAvatarUrl());
        builder.WithBotColor();

        ruser.AddFields(builder, RPGContext.RGuild);

        if (Context.User.IsUserRPGAdmin())
            builder.AddField(RPGContext.GetTranslate("json"), "```json\n" + JsonConvert.SerializeObject(ruser, Formatting.Indented) + "\n```");

        await ReplyAsync(null, false, builder.Build());
    }
    [Command("guild-info")]
    [Alias("guild", "gifo", "gfo", "gf", "gi", "ginfo", "guildinfo")]
    [RequireContext(ContextType.Guild)]
    [Summary("cmd.guild-info.description")]
    public async Task GuildInfo()
    {
        IGuild guild = Context.Guild;
        EmbedBuilder builder = new();
        builder.WithAuthor(guild.Name, guild.IconUrl);
        builder.WithBotColor();
        builder.AddField(RPGContext.GetTranslate("guild-owner"), await guild.GetOwnerAsync(), false);
        if (Context.User.IsUserRPGAdmin())
            builder.AddField(RPGContext.GetTranslate("json"), "```json\n" + JsonConvert.SerializeObject(RPGContext.RGuild, Formatting.Indented) + "```\n");
        await ReplyAsync(null, false, builder.Build());
    }
}