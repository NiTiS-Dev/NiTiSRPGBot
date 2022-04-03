using Discord;
using Discord.WebSocket;
using NiTiS.RPGBot;
using System;

namespace NiTiS.Discord.RPGBot.Services;

public class BotStartupService : IDisposable
{
	private readonly DiscordSocketClient client;
	private readonly Bot bot;
	public BotStartupService(IServiceProvider provider)
	{
		this.bot = provider.GetRequiredService<Bot>();
		this.client = provider.GetRequiredService<DiscordSocketClient>();

		client.LoginAsync(TokenType.Bot, bot.Token).Wait();
		client.StartAsync().Wait();
	}
	public void Dispose() => throw new NotImplementedException();
}
