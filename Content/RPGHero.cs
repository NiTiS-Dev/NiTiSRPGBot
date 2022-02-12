using Discord;

namespace NiTiS.RPGBot.Content;

public class RPGHero : IEmbedContent
{
    public void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        XPModule.AddFields(builder, rguild);
        builder.AddField("Money", Money);
        builder.AddField("Items", Inventory.Count);
    }
    [JsonProperty("xp_module")]
    public XPModule XPModule { get; private set; } = new();
    [JsonProperty("inventory")]
    public Inventory Inventory { get; private set; } = new();
    [JsonProperty("money")]
    public uint Money { get; set; } = 500;
}