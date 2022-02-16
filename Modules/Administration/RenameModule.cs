using Discord;
using Discord.Commands;
using Discord.Net;

namespace NiTiS.RPGBot.Modules.Administration;

public class RenameModule : BasicModule
{
    [Command("rename")]
    [RequireUserPermission(ChannelPermission.ManageRoles)]
    [RequireBotPermission(ChannelPermission.ManageRoles)]
    [Summary("cmd.rename.description")]
    public async Task Rename(IGuildUser? target = null, [Remainder] string? newName = null)
    {
        try
        {
            if (target is null)
            {
                await ReplyError(new ArgumentNullException(nameof(target)), RPGContext.Reference);
                return;
            }
            if (newName is null)
            {
                await ReplyError(new ArgumentNullException(nameof(newName)), RPGContext.Reference);
                return;
            }
            string oldName = target.Nickname;
            await target.ModifyAsync((a) =>
            {
                a.Nickname = newName;
            });
            string T_changes = RPGContext.GetTranslate("cmd.rename.changes");
            EmbedBuilder builder = new();
            builder.WithAuthor(target);
            builder.WithBotColor();
            builder.AddField(T_changes, $"`{oldName}` -> `{newName}`");
            await ReplyEmbed(builder, RPGContext.Reference);
        }
        catch (Exception ex)
        {
            if (ex is HttpException)
            {
                string T_accessDenied = RPGContext.GetTranslate("access-denied");
                string T_details = RPGContext.GetTranslate("details");
                await ReplyError(T_accessDenied, T_details + Environment.NewLine + $"```{ex.Message}```", RPGContext.Reference);
                return;
            }
            await ReplyError(ex, RPGContext.Reference);
        }
    }
}