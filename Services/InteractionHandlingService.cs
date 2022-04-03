using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NiTiS.Discord.RPGBot.Services;

public class InteractionHandlingService
{
	private readonly InteractionService service;
	private readonly DiscordSocketClient client;
	private readonly IServiceProvider provider;
	public InteractionHandlingService(IServiceProvider provider)
	{
		this.provider = provider;
		this.service = provider.GetRequiredService<InteractionService>();
		this.client = provider.GetRequiredService<DiscordSocketClient>();

		client.InteractionCreated += OnInteractionAsync;

		client.Ready += InitializeAsync;
	}
	public async Task InitializeAsync()
	{
		foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
		{
			await service.AddModulesAsync(ass, provider);
		}
#if DEBUG
		await service.AddCommandsToGuildAsync(client.GetGuild(865340901521489950), false);
#else
		await service.AddCommandsGloballyAsync(true);
#endif
	}
	private async Task OnInteractionAsync(SocketInteraction interaction)
	{
		_ = Task.Run(async () =>
		{
			SocketInteractionContext? context = new(client, interaction);
			await service.ExecuteCommandAsync(context, provider);
			Console.WriteLine($"Context created: {interaction.CreatedAt}");
		});
		await Task.CompletedTask;
		
	}
}
