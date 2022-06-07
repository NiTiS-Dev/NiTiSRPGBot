using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using NiTiS.Interaction.Services;
using NiTiS.RPGBot.Handler;
using NiTiS.IO;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace BotRunner;

public static class Runner
{
	private static readonly DiscordSocketConfig socketConfig;
	private static readonly DiscordSocketClient client;
	private static readonly InteractionService interaction;
	private static readonly InteractionHandler interactionHandler;
	private static readonly IServiceProvider provider;
	public static void Main()
	{
		client.Log += LogAsync;

		provider.GetRequiredService<InteractionHandler>()!.InitializeAsync().Wait();

		client.LoginAsync(TokenType.Bot, new File("token").ReadText()).Wait();
		client.StartAsync().Wait();

		Task.Delay(Timeout.Infinite).Wait();
	}
	private static async Task LogAsync(LogMessage message)
	{
		await Task.Run(() =>
		{
			Console.WriteLine(message);
		});
	}
	static Runner()
	{
		socketConfig = new()
		{
			GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
			AlwaysDownloadUsers = true,
		};

		client = new(socketConfig);
		interaction = new(client);

		provider = new ServiceCollection()
			.AddInstance(socketConfig)
			.AddInstance(client)
			.AddInstance(interaction)
			.Build();

		interactionHandler = new(provider);

		(provider as ServiceProvider)!.AddService(interactionHandler);
	}
}