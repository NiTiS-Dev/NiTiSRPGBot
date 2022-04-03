using Discord.Interactions;
using System.Threading.Tasks;

namespace NiTiS.Discord.RPGBot.Modules;

public class SlashModule : InteractionModuleBase<SocketInteractionContext>
{
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
