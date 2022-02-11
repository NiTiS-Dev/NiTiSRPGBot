using Discord;

namespace NiTiS.RPGBot.Content;

public class RPGHero : IEmbedContent
{
    public void AddFields(EmbedBuilder builder)
    {
    }
    [JsonProperty("xp_module")]
    public XPModule XPModule { get; set; } = new();
}