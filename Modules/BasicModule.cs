using Discord.Commands;
using NiTiS.RPGBot;
using System;
using System.Threading.Tasks;

namespace NiTiS.Discord.RPGBot.Modules;

public class BasicModule : ModuleBase<SocketCommandContext>
{
    private readonly Bot bot;
	public BasicModule(IServiceProvider provider)
	{
        this.bot = provider.GetRequiredService<Bot>();
	}
    [Command("info")]
    public async Task InfoAsync()
    {
        string msg = "Test message";
        await ReplyAsync(msg);
    }
    [Command("shutdown")]
    public async Task Shutdown()
	{
        await Context.Message.DeleteAsync();
        bot.Dispose();
	}
}
