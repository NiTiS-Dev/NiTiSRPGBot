using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Diagnostics;
using NiTiS.Core.Collections;
using Newtonsoft.Json;
using NiTiS.RPGBot.Content;

namespace NiTiS.RPGBot.Modules;

public class SuperUserModule : ModuleBase<SocketCommandContext>
{
    [Command("reg-item")]
    [Alias("r-i", "ri", "reg-i")]
    public Task RegistryItem(string id, Rarity rarity = Rarity.Common)
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return Task.CompletedTask;

        if (String.IsNullOrWhiteSpace(id))
        {
            return ReplyAsync("Err: id is null or whitespace!");
        }
        Item item = new Item(id);
        item.Rarity = rarity;
        Library.Registry(item);
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor();
        builder.WithBotColor();
        builder.AddField("Type", nameof(Item), true);
        builder.AddField("ID", id, false);
        builder.AddField("Rarity", rarity, false);
        builder.AddField("Json", JsonConvert.SerializeObject(item, Formatting.Indented), false);
        return ReplyAsync(null, false, builder.Build());
    }
}