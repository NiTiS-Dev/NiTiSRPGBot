using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
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

		client.Ready += InitializeAsync;
	}
	public async Task InitializeAsync()
	{
		service.SlashCommandExecuted += CommandExecuted;
		service.ContextCommandExecuted += ContextCommandExecuted; ;
		service.ComponentCommandExecuted += ComponentCommandExecuted;

		client.IntegrationCreated += InteractionCreated;
		foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
		{
			await service.AddModulesAsync(ass, provider);
		}
		await service.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
#if DEBUG
		IReadOnlyCollection<RestGuildCommand> commands = await service.AddCommandsToGuildAsync(client.GetGuild(865340901521489950), false);
		if (commands.Count == 0)
		{
			Console.WriteLine("Commands wasnt registred");
		}
		foreach(RestGuildCommand cmd in commands)
		{
			Console.WriteLine("Command: " + cmd);
		}
#else
		await service.AddCommandsGloballyAsync(true);
#endif
	}

	private async Task InteractionCreated(IIntegration arg)
	{
		if (arg is SocketInteraction socketInteraction)
		{
			await HandleInteraction(socketInteraction);
		}
	}
	private Task ComponentCommandExecuted(ComponentCommandInfo arg1, IInteractionContext arg2, IResult arg3) => throw new NotImplementedException();
	private Task ContextCommandExecuted(ContextCommandInfo arg1, IInteractionContext arg2, IResult arg3) => throw new NotImplementedException();
	private Task CommandExecuted(SlashCommandInfo arg1, IInteractionContext arg2, IResult arg3) => throw new NotImplementedException();
	private async Task HandleInteraction(SocketInteraction arg)
	{
		try
		{
			// create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
			SocketInteractionContext? ctx = new(client, arg);
			await service.ExecuteCommandAsync(ctx, provider);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			if (arg.Type == InteractionType.ApplicationCommand)
			{
				await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
			}
		}
	}
}
