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
        EmbedBuilder builder = new();
        try
        {
            WichmannRandom rand = new( (ushort)DateTime.Now.Millisecond);
            if(num1 is null)
            {
                return ReplyError(RPGContext.GetTranslate("cmd.random.no-arguments"), null);
            }
            int out3 = 0;
            if(Int32.TryParse(num1, out var out1))
            {
                if(Int32.TryParse(num2, out var out2))
                {
                    out3 = rand[out1, out2];
                }
                else
                {
                    out3 = rand[out1];
                }
            }
            else
            {
                out3 = rand[0, 5];
            }
            return ReplyAsync(out3.ToString());
        }catch (Exception ex)
        {
            return ReplyError(ex);
        }
        return ReplyEmbed(builder);
    }
}