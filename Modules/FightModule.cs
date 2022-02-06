using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules;

public class FightModule : ModuleBase<SocketCommandContext>
{
    public Dictionary<RPGUser, RPGUser> battles = new();

    [Command("fight")]
    [Alias("f")]
    [RequireContext(ContextType.Guild)]
    public Task Fight(SocketGuildUser user = null)
    {
        SocketUser self = Context.User;
        if (user == null)
        {
            return ReplyAsync("Fight with who??? :thinking:");
        }
        if (user == self)
        {
            return ReplyAsync("Fight with yourself?");
        }
        if (user.IsBot)
        {
            return ReplyAsync("Fight with bot is dirty trick!");
        }

        RPGUser enemy = user.ToRPGUser();
        RPGUser rself = self.ToRPGUser();

        if (battles.ContainsKey(enemy))
        {
            return ReplyAsync("Enemy already have battle request");
        }
        else
        {
            IUserMessage msg = ReplyAsync($"{user.Mention} if you wants to fight with {self.Mention} pass `{SingletonManager.GetInstance<BotClient>()?.Prefix}accept`").GetAwaiter().GetResult();
            battles.Add(enemy, rself);
            Task.Delay(1000 * 60).Wait();
            battles.Remove(enemy);
            return msg.DeleteAsync();
        }
    }
    [Command("accept")]
    [Alias("apply", "oki", "ok")]
    public async Task Apply()
    {
        RPGUser rself = Context.User.ToRPGUser();
        if (battles.ContainsKey(rself))
        {
            await ReplyAsync("Epic fight! *boom*");
            battles.Remove(rself);
        }
        else
        {
            await ReplyAsync("No one wants to fight with you");
        }
    }
}