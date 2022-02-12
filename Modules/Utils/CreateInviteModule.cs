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
    public async Task CreateInvite()
    {

    }
}
public sealed class NotGuildChannelException : Exception
{
    public NotGuildChannelException() : base()
    {

    }
}