using Discord;

namespace NiTiS.RPGBot.Content;

public class RPGHero : IEmbedContent
{
    public void AddFields(EmbedBuilder builder)
    {
        XPModule.AddFields(builder);
        builder.AddField("Items", Inventory.Count);
    }
    [JsonProperty("xp_module")]
    public XPModule XPModule { get; private set; } = new();
    [JsonProperty("inventory")]
    public Inventory Inventory { get; private set; } = new();
}