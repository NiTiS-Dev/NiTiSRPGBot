using Discord;
using Discord.Commands;
using System.Diagnostics;

namespace NiTiS.RPGBot.Modules.Utils;

public class UptimeModule : BasicModule
{
    [Command("uptime")]
    [Summary("cmd.uptime.description")]
    public async Task Ping()
    {
        EmbedBuilder builder = new();
        builder.WithBotAsAuthor().WithBotColor();
        if (!RPGContext.RUser.IsAdmin)
        {
            builder.WithBotErrorColor();
            builder.WithTitle(RPGContext.GetTranslate("error.rpg-admin-required"));
            await ReplyEmbed(builder);
            return;
        }
        builder.WithDescription(RPGContext.GetTranslate("information"));
        builder.AddField(RPGContext.GetTranslate("uptime"), GetUptime());
        builder.AddField(RPGContext.GetTranslate("heap-size"), GetHeapSize() + " MiB");
        await ReplyEmbed(builder);
    }
    private static string GetUptime() => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
    private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
}