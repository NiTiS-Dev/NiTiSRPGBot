using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NiTiS.RPGBot.Modules.Administration;

public class SetLanguageModule : ModuleBase<SocketCommandContext>
{
    [Command("setlang")]
    [Alias("lang")]
    [Summary("cmd.set-lang.description")]
    public async Task SetLang(string lang = null)
    {
        IGuild guild = Context.Guild;
        RPGGuild rguild = guild.ToRPGGuild();
        string T_aviableLanguages = rguild.GetTranslate("cmd.set-lang.aviable-langs");
        string T_langSetTo = rguild.GetTranslate("cmd.set-lang.lang-set-to");
        if (lang == null)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor(guild.Name, guild.IconUrl);
            builder.WithBotColor();
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
            await ReplyAsync(embed: builder.Build());
            return;
        }

        SocketGuildUser owner = Context.Guild.Owner;
        SocketUser self = Context.User;
        if (self.IsUserRPGAdmin() || self.Id == owner.Id)
        {
            string code = lang.ToLower();
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor(guild.Name, guild.IconUrl);
            builder.WithBotColor();
            if (Language.LanguageExists(code))
            {
                rguild.Lang = code;
                builder.WithDescription(String.Format(T_langSetTo, Language.GetLanguage(code).OriginalName));
            }


            await ReplyAsync(embed: builder.Build());
            return;
        }
        else
        {
            string T_notOwner = rguild.GetTranslate("cmd.error.not-owner");
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor(guild.Name, guild.IconUrl);
            builder.WithBotColor();
            builder.WithDescription(T_notOwner);
            await ReplyAsync(embed: builder.Build());
            return;
        }

    }
}