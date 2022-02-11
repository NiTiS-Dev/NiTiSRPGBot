using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Modules;

public class BotModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Alias("uptime")]
    public async Task Ping()
    {
        EmbedBuilder builder = new();
        builder.WithBotAsAuthor().WithBotColor();
        builder.WithDescription("Information");
        builder.AddField("Uptime", GetUptime());
        builder.AddField("Heap Size", GetHeapSize() + " MiB");
        await ReplyAsync(embed: builder.Build());
    }
    private static string GetUptime() => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
    private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
}