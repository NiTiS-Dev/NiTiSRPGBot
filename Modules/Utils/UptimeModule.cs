using Discord;
using Discord.Commands;
using System.Diagnostics;

namespace NiTiS.RPGBot.Modules.Utils;

public class UptimeModule : ModuleBase<SocketCommandContext>
{
    [Command("uptime")]
    [Summary("cmd.uptime.description")]
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