using Discord.Interactions;
using NiTiS.Discord.RPGBot.Services;
using System.Threading.Tasks;

namespace NiTiS.Discord.RPGBot.Modules;

public class SlashModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; }

    private InteractionHandlingService handler;
    public SlashModule(ServiceProvider provider)
    {
        this.handler = provider.GetRequiredService<InteractionHandlingService>()!;
        Commands = provider.GetRequiredService<InteractionService>()!;
    }

    [SlashCommand("rpginfo", "Information about rpg bot")]
    public async Task InfoAsync()
    {
        string msg = $@"Hi {Context.User}!\nYour id: `{Context.User.Id}`";
        await RespondAsync(msg);
    }
    [SlashCommand("echo", "Echo...")]
    public async Task EchoAsync(string message)
	{
        await DeleteOriginalResponseAsync();
        await Context.Channel.SendMessageAsync(message);
	}
}
