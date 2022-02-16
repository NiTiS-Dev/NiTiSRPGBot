using Discord.Commands;

namespace NiTiS.RPGBot.Modules.RPG;

public class CreateHeroModule : BasicModule
{
    [Command("create-hero")]
    [Alias("hero-create")]
    public async Task CrateHero()
    {
        if(RPGContext.RUser.Hero is null)
        {
            RPGContext.RUser.Hero = new RPGHero()
            {

            };
        }else
        {
            await ReplyError(ErrorType.HeroAllreadyCreated);
        }
    }
}