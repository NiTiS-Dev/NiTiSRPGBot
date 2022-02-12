using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules.Utils;

public class HelpModule : ModuleBase<SocketCommandContext>
{
    [Command("help")]
    [Alias("command-list", "hlp", "commands", "command", "cmds")]
    [Summary("cmd.help.description")]
    public async Task Help(string command = null)
    {
        CommandService service = SingletonManager.GetInstance<CommandService>();
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor().WithBotColor();
        foreach (var cmd in service.Commands)
        {
            string header = cmd.Name;
            var aliases = cmd.Aliases.Skip(1);
            if (aliases.Count() > 0)
            {
                header += " aka";
                foreach (string alias in aliases)
                {
                    header += ' ' + alias + ',';
                }
                header.TrimEnd(',');
            }
            builder.AddField(header, cmd.Summary ?? "None");
        }
        await ReplyAsync(embed: builder.Build());
    }
}