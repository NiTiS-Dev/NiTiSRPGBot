using Discord;
using NiTiS.Core.Collections;

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
}