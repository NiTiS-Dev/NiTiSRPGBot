using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public class HelpModule : BasicModule
{
    [Command("help")]
    [Alias("h", "commands")]
    [Summary("cmd.help.description")]
    public async Task Help(string? command = null)
    {
        try
        {
            CommandService service = SingletonManager.GetInstance<CommandService>();
            BotClient bot = SingletonManager.GetInstance<BotClient>();
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithBotAsAuthor().WithBotColor();
            if (command != null)
            {
                foreach(CommandInfo cmd in service.Commands)
                {
                    foreach(string alias in cmd.Aliases)
                    {
                        if (alias == command)
                        {
                            builder.WithTitle(bot.Prefix + cmd.Name);
                            builder.WithDescription(RPGContext.GetTranslate(cmd.Summary ?? "none"));
                            await ReplyEmbed(builder);
                            return;
                        }
                    }
                }
                await ReplyError(new CommandNotFoundException(command), RPGContext.Reference);
                return;
            }
            foreach (var cmd in service.Commands)
            {
                builder.AddField(bot.Prefix + cmd.Name, RPGContext.GetTranslate(cmd.Summary ?? "none"), true);
            }
            await ReplyEmbed(builder, RPGContext.Reference);
        }
        catch (Exception ex)
        {
            await ReplyError(ex.GetType().Name, ex.Message, RPGContext.Reference);
        }
    }
}

public sealed class CommandNotFoundException : Exception
{
    public CommandNotFoundException(string commandName) : base($"Command `{commandName}` not found")
    {

    }
}