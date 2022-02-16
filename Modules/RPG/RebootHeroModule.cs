using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NiTiS.RPGBot.Modules.RPG;

public class RebootHeroModule : BasicModule
{
    private static volatile Dictionary<ulong, IUserMessage> heroDelete = new();

    [Command("reboot-hero")]
    [Alias("reboot", "delete-hero", "delete", "hero-reboot")]
    public async Task RebootHero()
    {
        EmbedBuilder embedBuilder = new();
        embedBuilder.WithBotErrorColor();
        RPGUser ruser = RPGContext.RUser;
        if (ruser.Hero is null)
        {
            await ReplyError(ErrorType.HeroNotCreated);
            return;
        }
        if (!heroDelete.TryGetValue(Context.User.Id, out IUserMessage? outMessage))
        {
            embedBuilder.WithBotColor();
            embedBuilder.WithTitle(RPGContext.GetTranslate("cmd.reboot-hero.delete-hero"));
            embedBuilder.WithDescription(RPGContext.GetTranslate("cmd.reboot-hero.delete-hero.description"));
            embedBuilder.AddField(RPGContext.GetTranslate("cmd.reboot-hero.coin-amount"), RPGContext.RUser.Hero?.CalculateRebootCoins());
            ComponentBuilder builder = new();
            builder.WithButton(RPGContext.GetTranslate("cmd.reboot-hero.yes-delete"), "delete-hero", ButtonStyle.Danger);
            builder.WithButton(RPGContext.GetTranslate("cmd.reboot-hero.no-save"), "delete-self", ButtonStyle.Success);
            IUserMessage msg = await ReplyAsync(embed: embedBuilder.Build(), components: builder.Build());
            heroDelete.Add(Context.User.Id, msg);
        }
        else
        {
            embedBuilder.WithBotAsAuthor();
            embedBuilder.WithTitle(RPGContext.GetTranslate("cmd.reboot-hero.request-allready-exists"));
            await ReplyEmbed(embedBuilder, outMessage.Channel.Id == Context.Channel.Id ? new MessageReference(outMessage.Id) : null);
        }
        await Task.Delay(1000 * 35);
        if (heroDelete.ContainsKey(Context.User.Id))
        {
            var message = heroDelete[Context.User.Id];
            heroDelete.Remove(Context.User.Id);
            await message.DeleteAsync();
            return;
        }
    }

    public static async Task ButtonDeleteClicked(SocketInteraction interaction)
    {
        if (heroDelete.TryGetValue(interaction.User.Id, out IUserMessage? msg))
        {
            heroDelete.Remove(interaction.User.Id);
            await msg.DeleteAsync();
            RPGUser ruser = interaction.User.ToRPGUser();
            ruser.Hero = null;
            return;
        }
    }
    public static async Task ButtonCancelClicked(SocketInteraction interaction)
    {
        if (heroDelete.TryGetValue(interaction.User.Id, out IUserMessage? msg))
        {
            heroDelete.Remove(interaction.User.Id);
            await msg.DeleteAsync();
            return;
        }
    }
}