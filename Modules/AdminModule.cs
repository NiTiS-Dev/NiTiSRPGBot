﻿using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Diagnostics;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Content;

namespace NiTiS.RPGBot.Modules;

public class AdminModule : ModuleBase<SocketCommandContext>
{
    [Command("clear")]
    [Alias("clr","cler","clar")]
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(ChannelPermission.ManageMessages)]
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    public async Task Clear(Int32 count = 10)
    {
        Stopwatch stopWatch = Stopwatch.StartNew();

        SocketGuild guild = Context.Guild;
        ISocketMessageChannel channel = Context.Channel;
        var cachedMessages = channel.GetMessagesAsync(count);
        int deletedMessages = 0;
        
        await foreach(var messages in cachedMessages)
        {
            foreach(var message in messages)
            {
                await message.DeleteAsync();
                deletedMessages++;
            }
        }
        stopWatch.Stop();

        EmbedBuilder builder = new EmbedBuilder();
        builder.WithTitle("Result");
        builder.WithAuthor("RPGBot");

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
    [Command("uinfo")]
    [Alias("user","info","about","ufo", "ui")]
    [RequireContext(ContextType.Guild)]
    public async Task UserInfo()
    {
        SocketUser user = Context.User;
        RPGUser ruser = SingletonManager.GetInstance<SaveModule>().LoadUser(user.Id);
        EmbedBuilder builder = new EmbedBuilder();

        builder.WithAuthor(user.Username, user.GetAvatarUrl());
        builder.WithBotColor();
        
        builder.AddField("id", user.Id);
        builder.AddField("admin", ruser.IsAdmin);
        await ReplyAsync(null, false, builder.Build());
    }
    [Command("ginfo")]
    [Alias("guild", "gifo", "gfo", "gf")]
    [RequireContext(ContextType.Guild)]
    public async Task GuildInfo()
    {
        IGuild guild = Context.Guild;
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithAuthor(guild.Name, guild.IconUrl);
        builder.WithBotColor();
        await ReplyAsync(null, false, builder.Build());
    }
}
