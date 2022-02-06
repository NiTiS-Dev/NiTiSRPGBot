using Discord;
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
}