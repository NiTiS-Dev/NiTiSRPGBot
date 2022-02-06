using Discord.Commands;
using Discord;
using NiTiS.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Modules;

public class HelpModule : ModuleBase<SocketCommandContext>
{
    [Command("help")]
    [Alias("command-list", "hlp", "commands", "command", "cmds")]
    public async Task Help(string command = null)
    {
        CommandService service = SingletonManager.GetInstance<CommandService>();
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor().WithBotColor();
        foreach(var cmd in service.Commands)
        {
            string header = cmd.Name;
            if(cmd.Aliases.Count > 0)
            {
                header += " aka";
                foreach (string alias in cmd.Aliases)
                {
                    header += ' ' + alias + ',';
                }
                header.TrimEnd(',');
            }
            builder.AddField(header, cmd.Summary ?? "None");
        }
        await ReplyAsync(embed:builder.Build());
    }
}