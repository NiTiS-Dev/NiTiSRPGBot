using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NiTiS.RPGBot.Modules.Administration;

public class SetLanguageModule : BasicModule
{
    [Command("set-lang")]
    [Alias("lang")]
    [Summary("cmd.set-lang.description")]
    public async Task SetLang(string? lang = null)
    {
        IGuild guild = Context.Guild;
        RPGGuild rguild = guild.ToRPGGuild();
        string T_aviableLanguages = rguild.GetTranslate("cmd.set-lang.aviable-langs");
        EmbedBuilder builder = new EmbedBuilder();
        builder.WithGuildAsAuthor(guild);
        builder.WithBotColor();
        if (lang == null)
        {
            builder.WithBotAsAuthor();
            builder.WithDescription(T_aviableLanguages);
            foreach (Language lng in Language.Languages)
            {
                if (lng.EnglishName == lng.OriginalName)
                {
                    builder.AddField(lng.EnglishName, lng.Code, false);
                    continue;
                }
                builder.AddField(lng.EnglishName + " | " + lng.OriginalName, lng.Code, false);
            }
            await ReplyEmbed(builder);
            return;
        }

        SocketGuildUser owner = Context.Guild.Owner;
        SocketUser self = Context.User;
        if (self.IsUserRPGAdmin() || self.Id == owner.Id) //If you rpgAdmin or Owner
        {
            string code = lang.ToLower();
            if (Language.TryGetValue(code, out Language lng))
            {
                rguild.Lang = lng.Code;
                string T_langSetTo = rguild.GetTranslate("cmd.set-lang.lang-set-to");
                builder.WithDescription(String.Format(T_langSetTo, Language.GetLanguage(lng.Code).OriginalName ?? "erro"));
            }
            else
            {
                await ReplyError(new LanguageNotFound(code), RPGContext.Reference);
                return;
            }


            await ReplyEmbed(builder);
            return;
        }
        else
        {
            await ReplyError(new NotServerOwner(), RPGContext.Reference);
            return;
        }

    }
}

public sealed class LanguageNotFound : Exception
{
    public LanguageNotFound(string lang) : base($"Language `{lang}` not found")
    {

    }
}
public sealed class NotServerOwner : Exception
{
    public NotServerOwner() : base("Server Creator Needed")
    {

    }
} 