using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;
using System.Diagnostics;

namespace NiTiS.RPGBot.Modules.Administration;

public class ClearModule : BasicModule
{
    [Command("clear")]
    [Alias("clr")]
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(ChannelPermission.ManageMessages)]
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    [Summary("cmd.clear.description")]
    public async Task Clear(Int32 count = 10)
    {
        if(count <= 0 || count > 250)
        {
            await ReplyError(new ArgumentOutOfRangeException(nameof(count)), null);
            return;
        }
        Stopwatch stopWatch = Stopwatch.StartNew();

        RPGGuild rguild = RPGContext.RGuild;
        ISocketMessageChannel channel = Context.Channel;
        var cachedMessages = channel.GetMessagesAsync(count);
        int deletedMessages = 0;

        await foreach (var messages in cachedMessages)
        {
            foreach (var message in messages)
            {
                await message.DeleteAsync();
                deletedMessages++;
            }
        }
        stopWatch.Stop();

        EmbedBuilder builder = new();
        string T_result = rguild.GetTranslate("result");
        builder.WithTitle(T_result);

        string T_messagesDeleted = rguild.GetTranslate("cmd.clear.messages-deleted");
        string T_messagesDeletedFormat = rguild.GetTranslate("cmd.clear.messages-deleted.format");
        string T_timeElapsed = rguild.GetTranslate("cmd.clear.time-elapsed");
        string T_timeElapsedFormat = rguild.GetTranslate("cmd.clear.time-elapsed.format");
        builder.AddField(T_messagesDeleted, String.Format(T_messagesDeletedFormat, deletedMessages, count));
        builder.AddField(T_timeElapsed, String.Format(T_timeElapsedFormat, stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds)); // {0}m {1}s

        builder.WithBotAsAuthor();
        builder.WithBotColor();
        IUserMessage deleteMessage = await ReplyAsync(null, false, builder.Build());

        await Task.Delay(1000 * 15); //Wait 15 sec
        await deleteMessage.DeleteAsync();
    }
}