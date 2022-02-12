using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;
using System.Diagnostics;

namespace NiTiS.RPGBot.Modules.Administration;

public class ClearModule : ModuleBase<SocketCommandContext>
{
    [Command("clear")]
    [Alias("clr", "cler", "clar")]
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(ChannelPermission.ManageMessages)]
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    [Summary("cmd.clear.description")]
    public async Task Clear(Int32 count = 10)
    {
        Stopwatch stopWatch = Stopwatch.StartNew();

        SocketGuild guild = Context.Guild;
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

        EmbedBuilder builder = new EmbedBuilder();
        builder.WithTitle("Result");

        EmbedFieldBuilder msgDeletedBuilder = new EmbedFieldBuilder();
        msgDeletedBuilder.WithName("Message Deleted");
        msgDeletedBuilder.WithValue($"{deletedMessages}/{count}");

        EmbedFieldBuilder timeElapsedBuilder = new EmbedFieldBuilder();
        timeElapsedBuilder.WithName("Time Has Passed");
        timeElapsedBuilder.WithValue($"{stopWatch.Elapsed.Minutes}m {stopWatch.Elapsed.Seconds}s");

        builder.WithFields(msgDeletedBuilder, timeElapsedBuilder);
        builder.WithColor(SingletonManager.GetInstance<RPGBot>().CommandColor);
        builder.WithBotAsAuthor();
        builder.WithBotColor();
        IUserMessage deleteMessage = ReplyAsync(null, false, builder.Build()).GetAwaiter().GetResult();

        await Task.Delay(10000); //Wait 10 sec
        await deleteMessage.DeleteAsync();
    }
}