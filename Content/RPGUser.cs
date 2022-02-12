using Discord;

namespace NiTiS.RPGBot.Content;

public class RPGUser : IEmbedContent
{
    [JsonProperty("id")]
    public ulong Id { get; set; } = ulong.MinValue;
    [JsonProperty("admin")]
    public bool IsAdmin { get; set; } = false;
    [JsonProperty("hero")]
    public RPGHero Hero { get; set; }

    public RPGUser(ulong id)
    {
        this.Id = id;
    }

    public void AddFields(EmbedBuilder builder)
    {
        
    }
}
