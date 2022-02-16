using Discord;
using Discord.Commands;
using NiTiS.Core.Math;

namespace NiTiS.RPGBot.Modules.Utils;

public class RandomModule : BasicModule
{
    [Command("random")]
    [Alias("rand", "rnd")]
    public Task Random(string? num1 = null, string? num2 = null)
    {
        try
        {
            WichmannRandom rand = new( (ushort)DateTime.Now.Millisecond);
            int out3 = 0;
            if(num1 is null)
            {
                out3 = rand[1, 6];
            }
            if(Int32.TryParse(num1, out var out1))
            {
                if(Int32.TryParse(num2, out var out2))
                {
                    out3 = rand[out1, out2];
                }
                else
                {
                    if(num2 is not null)
                    {
                        return ReplyError(RPGContext.GetTranslate("cmd.random.invalid-agruments"), null);
                    }
                    out3 = rand[out1];
                }
            }
            else
            {
                if (num1 is not null)
                {
                    return ReplyError(RPGContext.GetTranslate("cmd.random.invalid-agruments"), null);
                }
            }
            EmbedBuilder builder = new();
            builder.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl());
            builder.WithBotColor();
            builder.WithTitle(String.Format(RPGContext.GetTranslate("cmd.random.result.format"), out3));
            return ReplyEmbed(builder);
        }catch (Exception ex)
        {
            return ReplyError(ex);
        }
    }
}