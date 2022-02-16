using Discord;
using Discord.Commands;
using NiTiS.Core.Additions;
using NiTiS.Core.Collections;

namespace NiTiS.RPGBot.Modules;

public abstract class BasicModule : ModuleBase<SocketCommandContext>
{
    public RPGCommandContext RPGContext { get; private set; }
    protected async Task<IUserMessage> ReplyError(ErrorType errorType)
    {
        EmbedBuilder builder = new();
        builder.WithBotAsAuthor().WithBotErrorColor();
        builder.WithTitle(RPGContext.GetTranslate(errorType.GetEnumValueName()));
        builder.WithDescription(RPGContext.GetTranslate(errorType.GetEnumValueDescription()));
        return await ReplyAsync(embed: builder.Build());
    }
    protected async Task<IUserMessage> ReplyError(string? title, string? desc, MessageReference? reference = null)
    {
        EmbedBuilder builder = new();
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        builder.WithColor(bot.ErrorColor);
        builder.WithBotAsAuthor();
        builder.WithTitle(title ?? "");
        builder.WithDescription(desc ?? "");
        return await ReplyAsync(embed: builder.Build(), messageReference: reference);
    }
    protected async Task<IUserMessage> ReplyError(Exception exception, MessageReference? reference = null)
    {
        EmbedBuilder builder = new();
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        builder.WithColor(bot.ErrorColor);
        builder.WithBotAsAuthor();
        builder.WithTitle(exception.GetType().Name);
        builder.WithDescription(exception.Message);
        return await ReplyEmbed(builder, reference);
    }
    protected async Task<IUserMessage> ReplyEmbed(EmbedBuilder? builder = null, MessageReference? reference = null) => await ReplyEmbed(builder?.Build(), reference);
    protected async Task<IUserMessage> ReplyEmbed(Embed? embed = null, MessageReference? reference = null)
    {
        return await ReplyAsync(embed: embed, messageReference: reference);
    }
    public BasicModule()
    {
        RPGContext = new(this);
    }
    public class RPGCommandContext
    {
        private readonly Lazy<ICommandContext> context;
        private RPGUser ruser;
        private RPGGuild rguild;
        public RPGUser RUser => ruser ??= context.Value.User.ToRPGUser();
        public RPGGuild RGuild => rguild ??= context.Value.Guild.ToRPGGuild();
        public string GetTranslate(string key) => RGuild.GetTranslate(key);
        public string GetTranslate(bool key) => RGuild.GetTranslate(key ? "bool.true" : "bool.false");
        public MessageReference Reference => new(context.Value.Message.Id);
#pragma warning disable CS8618
        public RPGCommandContext(ModuleBase<SocketCommandContext> module)
#pragma warning restore CS8618
        {
            context = new(() =>
           {
               return module.Context;
           });
        }
    }
}