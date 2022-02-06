using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Diagnostics;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules;

public class SuperUserModule : ModuleBase<SocketCommandContext>
{
    [Command("reg-item")]
    [Alias("r-i", "ri", "reg-i")]
    public Task RegistryItem(string id, Rarity rarity = Rarity.Common, int sellCost = 100)
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return Task.CompletedTask;

        if (String.IsNullOrWhiteSpace(id))
        {
            return ReplyAsync("Err: id is null or whitespace!");
        }
        Item item = new Item(id);
        item.Rarity = rarity;
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor();
        builder.WithBotColor();
        builder.AddField("Type", nameof(Item), true);
        builder.AddField("ID", id, true);
        builder.AddField("Rarity", rarity, false);
        builder.AddField("Json", JsonConvert.SerializeObject(item, Formatting.Indented), false);
        return ReplyAsync(null, false, builder.Build());
    }
    [Command("reg-weapon")]
    [Alias("r-w", "rw", "reg-w")]
    public Task RegistryWeapon(string id, Rarity rarity = Rarity.Common, int sellCost = 100, int damage = 1, WeaponType type = WeaponType.Sword)
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return Task.CompletedTask;

        if (String.IsNullOrWhiteSpace(id))
        {
            return ReplyAsync("Err: id is null or whitespace!");
        }
        Weapon weapon = new Weapon(id, damage, sellCost);
        weapon.Rarity = rarity;
        weapon.Type = type;
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithBotAsAuthor();
        builder.WithBotColor();
        builder.AddField("Type", nameof(Weapon), true);
        builder.AddField("ID", id, true);
        builder.AddField("Rarity", rarity, true);
        builder.AddField("Weapon Type", type, true);
        builder.AddField("Damage", damage, true);
        builder.AddField("Json", JsonConvert.SerializeObject(weapon, Formatting.Indented), false);
        return ReplyAsync(null, false, builder.Build());
    }
    [Command("library-items")]
    [Alias("l-items", "l-i", "li")]
    public async Task LibrarySearchItems()
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return;
        foreach(Item item in Library.SearchAll<Item>())
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithBotAsAuthor();
            builder.WithBotColor();
            builder.AddField("ID", item.ID, true);
            builder.AddField("Rarity", item.Rarity, true);
            builder.AddField("Sell Cost", item.SellCost, true);
            await ReplyAsync(null, false, builder.Build());
        }
    }
    [Command("library-weapons")]
    [Alias("l-weapons", "l-w", "lw")]
    public async Task LibrarySearchWeapons()
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return;
        foreach (Weapon weapon in Library.SearchAll<Weapon>())
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithBotAsAuthor();
            builder.WithBotColor();
            builder.AddField("ID", weapon.ID, true);
            builder.AddField("Rarity", weapon.Rarity, true);
            builder.AddField("Sell Cost", weapon.SellCost, true);
            builder.AddField("Weapon Type", weapon.Type, true);
            builder.AddField("Damage", weapon.Damage, true);
            await ReplyAsync(null, false, builder.Build());
        }
    }
}