using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NiTiS.Discord.RPGBot.Services;

public class CommandHandlingService
{
	private readonly CommandService commands;
	private readonly DiscordSocketClient discord;
	private readonly IServiceProvider services;
	public CommandHandlingService(IServiceProvider services)
	{
		this.commands = services.GetRequiredService<CommandService>();
		this.discord = services.GetRequiredService<DiscordSocketClient>();
		this.services = services;

        commands.CommandExecuted += CommandExecutedAsync;
        discord.MessageReceived += MessageReceivedAsync;

        InitializeAsync().Wait();
	}
    public async Task InitializeAsync()
    {
        foreach(Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
		{
            await commands.AddModulesAsync(ass, services);
		}
    }

    public async Task MessageReceivedAsync(SocketMessage rawMessage)
    {
        if (rawMessage is not SocketUserMessage message)
            return;
        if (message.Source != MessageSource.User)
            return;

        int argPos = 0;
        if (!message.HasStringPrefix("::", ref argPos))
            return;

        SocketCommandContext context = new(discord, message);

        await commands.ExecuteAsync(context, argPos, services);
    }

    public static async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
    {
        if (!command.IsSpecified)
            return;

        if (result.IsSuccess)
            return;

        await context.Channel.SendMessageAsync($"```\nError:\n{result}\n```");
    }
}
