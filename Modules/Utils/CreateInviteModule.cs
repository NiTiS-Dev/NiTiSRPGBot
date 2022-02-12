using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public class CreateInviteModule : BasicModule
{
    [Command("create-invite")]
    [Alias("invite")]
    [Summary("cmd.create-invite.description")]
    [RequireBotPermission(ChannelPermission.CreateInstantInvite)]
    [RequireContext(ContextType.Guild)]
    [RequireUserPermission(ChannelPermission.CreateInstantInvite)]
    public async Task CreateInvite(IChannel channel = null)
    {
        channel ??= Context.Channel;
        if (channel is not INestedChannel nestedChannel)
        {
            await ReplyError(new ArgumentException("Channel must be in a guild and category"), RPGContext.Reference);
            return;
        }
        IInviteMetadata invite = await nestedChannel.CreateInviteAsync(null);
        string T_inviteCreated = RPGContext.GetTranslate("cmd.create-invite.invite-created");
        EmbedBuilder builder = new();
        builder.WithBotAsAuthor().WithBotColor();
        builder.WithTitle(T_inviteCreated);
        builder.WithDescription(invite.Url);
        await ReplyEmbed(builder);
    }
}
public sealed class NotGuildChannelException : Exception
{
    public NotGuildChannelException() : base()
    {

    }
}