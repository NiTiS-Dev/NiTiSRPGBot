// The NiTiS-Dev licenses this file to you under the MIT license.

namespace NiTiS.RPGBot.Handler;
public class InteractionHandler
{
	private readonly DiscordSocketClient client;
	private readonly InteractionService handler;
	private readonly IServiceProvider provider;
	public InteractionHandler(IServiceProvider provider)
	{
		this.client = provider.GetRequiredService<DiscordSocketClient>()!;
		this.handler = provider.GetRequiredService<InteractionService>()!;
		this.provider = provider;
	}

	public async Task InitializeAsync()
	{
		client.InteractionCreated += HandleInteraction;
		client.Ready += ReadyAsync;
		handler.Log += LogAsync;

		await handler.AddModulesAsync(typeof(Module.HelpModule).Assembly, provider);
	}

	private async Task LogAsync(LogMessage log)
		=> Console.WriteLine(log);

	private async Task ReadyAsync()
	{
		if (Workspace.IsDebug)
			await handler.RegisterCommandsToGuildAsync(Workspace.GuildID, true);
		else
			await handler.RegisterCommandsGloballyAsync(true);
	}

	private async Task HandleInteraction(SocketInteraction interaction)
	{
		try
		{
			SocketInteractionContext? context = new(client, interaction);

			IResult? result = await handler.ExecuteCommandAsync(context, provider);

			if (!result.IsSuccess)
				switch (result.Error)
				{
					case InteractionCommandError.UnmetPrecondition:
						break;
					default:
						break;
				}
		}
		catch
		{
			if (interaction.Type is InteractionType.ApplicationCommand)
				await interaction.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
		}
	}
}