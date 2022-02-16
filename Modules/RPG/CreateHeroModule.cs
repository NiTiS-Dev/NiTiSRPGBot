using Discord;
using Discord.Commands;
using Random = NiTiS.Core.Math.WichmannRandom;

namespace NiTiS.RPGBot.Modules.RPG;

public class CreateHeroModule : BasicModule
{
    [Command("create-hero")]
    [Alias("hero-create")]
    public async Task CrateHero()
    {
        if (RPGContext.RUser.Hero is null)
        {
            Random random = new((ushort)DateTime.Now.Millisecond);
            int rand1 = random.Next(7);
            int rand2 = random.Next(2) + 1;
            Race race = (Race)rand1;
            Gender gender = (Gender)rand2;
            EmbedBuilder builder = new();
            builder.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl()).WithBotColor();
            builder.AddField("Rand 1", $"{race} : {rand1}");
            builder.AddField("Rand 2", $"{gender} : {rand2}");
            RPGContext.RUser.Hero = new RPGHero()
            {
                Gender = gender,
                Race = race,
            };
            await ReplyEmbed(builder);
            return;
        }
        else
        {
            await ReplyError(ErrorType.HeroAllreadyCreated);
        }
    }
}