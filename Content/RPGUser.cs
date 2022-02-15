using Discord;

namespace NiTiS.RPGBot.Content;

public class RPGUser : IEmbedContent
{
    [JsonProperty("id")]
    public ulong Id { get; set; } = ulong.MinValue;
    [JsonProperty("admin")]
    public bool IsAdmin { get; set; } = false;
    [JsonProperty("reset_shards")]
    public uint ResetShards { get; set; } = 1;
    [JsonProperty("hero")]
    public RPGHero? Hero { get; set; }

    public RPGUser(ulong id)
    {
        this.Id = id;
    }

    public void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        string T_id = rguild.GetTranslate("id");
        string T_admin = rguild.GetTranslate("admin");

        builder.AddField(T_id, Id);
        builder.AddField(T_admin, rguild.GetTranslate(IsAdmin ? "bool.true" : "bool.false"));
    }
}
