﻿using Discord;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Content;

namespace NiTiS.RPGBot;

public static class NiTiSBotExtensions
{
    public static EmbedBuilder WithBotAsAuthor(this EmbedBuilder builder)
    {
        var user = SingletonManager.GetInstance<RPGBot>().Self;
        builder.WithAuthor(user.Username, user.GetAvatarUrl());
        return builder;
    }
    public static EmbedBuilder WithBotColor(this EmbedBuilder builder)
    {
        var bot = SingletonManager.GetInstance<RPGBot>();
        builder.WithColor(bot.CommandColor);
        return builder;
    }
    public static RPGUser ToRPGUser(this IUser user)
    {
        return SingletonManager.GetInstance<SaveModule>().LoadUser(user.Id);
    }
    public static bool IsUserRPGAdmin(this IUser user)
    {
        return user.ToRPGUser().IsAdmin;
    }
    public static void Registry(this IRegistrable reg)
    {
        Library.Registry(reg);
    }
    public static string GetTranslate(this RPGGuild guild, string key)
    {
        return Language.GetTranslate(guild, key);
    }
    public static RPGGuild ToRPGGuild(this IGuild guild)
    {
        return SingletonManager.GetInstance<SaveModule>().LoadGuild(guild.Id);
    }
}