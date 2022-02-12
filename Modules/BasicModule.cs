using Discord;
using Discord.Commands;
using NiTiS.Core.Collections;
using NiTiS.RPGBot.Modules.Utils;

namespace NiTiS.RPGBot.Modules;

public abstract class BasicModule : ModuleBase<SocketCommandContext>
{
    public RPGCommandContext RPGContext { get; private set; } 
    protected async Task<IUserMessage> ReplyError(string title, string desc, MessageReference? reference)
    {
        EmbedBuilder builder = new();
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        builder.WithColor(bot.ErrorColor);
        builder.WithBotAsAuthor();
        builder.WithTitle(title);
        builder.WithDescription(desc);
        return await ReplyAsync(embed: builder.Build(), messageReference: reference);
    }
    public async Task<IUserMessage> ReplyError(Exception exception, MessageReference? reference)
    {
        EmbedBuilder builder = new();
        RPGBot bot = SingletonManager.GetInstance<RPGBot>();
        builder.WithColor(bot.ErrorColor);
        builder.WithBotAsAuthor();
        builder.WithTitle(exception.GetType().Name);
        builder.WithDescription(exception.Message);
        return await ReplyEmbed(builder, reference);
    }
    protected async Task<IUserMessage> ReplyEmbed(EmbedBuilder builder, MessageReference? reference = null) => await ReplyEmbed(builder.Build(), reference);
    protected async Task<IUserMessage> ReplyEmbed(Embed embed, MessageReference? reference = null)
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
        public MessageReference Reference => new(context.Value.Message.Id);
        public RPGCommandContext(ModuleBase<SocketCommandContext> module)
        {
            context = new( () =>
            {
                return module.Context;
            });
        }
    }
}