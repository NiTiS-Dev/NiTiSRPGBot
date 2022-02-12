using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.RPG;

public class FightModule : ModuleBase<SocketCommandContext>
{
    public static volatile Dictionary<RPGUser, RPGUser> battles = new();

    [Command("fight")]
    [Alias("f")]
    [RequireContext(ContextType.Guild)]
    [Summary("cmd.fight.description")]
    public async Task Fight(SocketGuildUser user = null)
    {
        SocketUser self = Context.User;
        if (user == null)
        {
            await ReplyAsync("Fight with who??? :thinking:");
            return;
        }
        /*if (user == self)
        {
            await ReplyAsync("Fight with yourself?");
            return;
        }*/
        if (user.IsBot)
        {
            await ReplyAsync("Fight with bot is dirty trick!");
            return;
        }

        RPGUser enemy = user.ToRPGUser();
        RPGUser rself = self.ToRPGUser();

        if (battles.ContainsKey(enemy))
        {
            await ReplyAsync("Enemy already have battle request");
            return;
        }
        else
        {
            IUserMessage msg = ReplyAsync($"{user.Mention} if you wants to fight with {self.Mention} pass `{SingletonManager.GetInstance<BotClient>()?.Prefix}accept` you have only 60s").GetAwaiter().GetResult();
            battles.Add(enemy, rself);
            Console.WriteLine(battles.Count);
            await Task.Delay(1000 * 60);
            battles.Remove(enemy);
            await msg.DeleteAsync();
            return;
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