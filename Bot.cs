using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using NiTiS.Discord.RPGBot.Services;
using System;

namespace NiTiS.RPGBot;
public class Bot : IDisposable
{
	private readonly DiscordSocketClient client;
	private ServiceProvider? services;
	public Directory RuntimeDirectory { get; }
	public string Token { get; }
	public bool IsExit { get; private set; } = false;
	public Bot(Directory runtimeDirectory, string token)
	{
		RuntimeDirectory = runtimeDirectory;
		Token = token;
		this.client = new();
	}
	public void Startup()
	{
		this.services = ConfigureServices();
	}
	public void Dispose()
	{
		this.client.LogoutAsync();
		IsExit = true;
	}

	private ServiceProvider ConfigureServices()
	{
		return new ServiceCollection()
			.AddInstance(this)
			.AddInstance(this.client)
			.AddInstance(new InteractionService(this.client))
			.AddSingleton<CommandService>()
			.AddSingleton<BotLogService>()
			.AddSingleton<BotStartupService>()
			.AddSingleton<CommandHandlingService>()
			.AddSingleton<InteractionHandlingService>()
			.Build();
	}
}