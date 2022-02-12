using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Modules.Administration;

public class RenameModule : ModuleBase<SocketCommandContext>
{
    [Command("rename")]
    [RequireUserPermission(ChannelPermission.ManageRoles)]
    [RequireBotPermission(ChannelPermission.ManageRoles)]
    [Summary("cmd.rename.description")]
    public async Task Rename(IGuildUser target, [Remainder]string newName)
    {
        
        if(target == null)
        {
            // TODO: Create exception in chat
            return;
        }
        if(newName == null)
        {

            return;
        }
        await target.ModifyAsync((a) =>
       {
           a.Nickname = newName;
       });
    }
}