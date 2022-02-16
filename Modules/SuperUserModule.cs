using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules;

public class SuperUserModule : BasicModule
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
        Item item = new(id)
        {
            Rarity = rarity
        };
        EmbedBuilder builder = new();
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
        Weapon weapon = new(id, damage, sellCost);
        weapon.Rarity = rarity;
        weapon.Type = type;
        EmbedBuilder builder = new();
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
        foreach (Item item in Library.SearchAll<Item>())
        {
            EmbedBuilder builder = new();
            item.AddFields(builder, RPGContext.RGuild);
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
            EmbedBuilder builder = new();
            weapon.AddFields(builder, RPGContext.RGuild);
            await ReplyEmbed(builder);
        }
    }
    [Command("where")]
    [Alias("whr")]
    public async Task Where()
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return;

        await ReplyAsync(message: SingletonManager.GetInstance<SaveModule>().DataDirectory);
    }
    [Command("getkey")]
    [Alias("key")]
    public async Task GetKey(string key)
    {
        EmbedBuilder builder = new();
        builder.WithBotAsAuthor();
        builder.WithBotColor();
        foreach (Language lang in Language.Languages)
        {
            string value = "None";
            if (lang.Exists(key))
            {
                value = lang.GetValue(key);
            }
            builder.AddField(lang.EnglishName, value, true);
        }
        await ReplyAsync(null, false, builder.Build());
    }
    [Command("add-item")]
    [Alias("a-i", "ai")]
    public async Task AddItem(string? itemID = null, uint count = 1)
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return;
        RPGUser user = RPGContext.RUser;
        if (user.Hero is null)
        {
            await ReplyError(ErrorType.HeroNotCreated);
            return;
        }
        else
        {
            RPGHero hero = user.Hero;
            if (itemID is not null)
            {
                Item? item = Library.Search<Item>(itemID);
                if (item is null)
                {
                    await ReplyError(ErrorType.RegistryDoesntExists);
                    return;
                }
                hero.Inventory.AddItem(item, 1);

                return;
            }
            await ReplyError(ErrorType.NoParameters);
        }
    }

    [Command("throw")]
    [Alias("thr", "err", "error")]
    public async Task ThrowError(ushort id = 0)
    {
        if (!Context.User.IsUserRPGAdmin()) //Admin command
            return;
        ErrorType errorType = (ErrorType)id;
        await ReplyError(errorType);
    }
}