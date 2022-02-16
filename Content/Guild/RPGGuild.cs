namespace NiTiS.RPGBot.Content.Guild;

public class RPGGuild
{
    [JsonProperty("id")]
    public ulong Id { get; set; }

    [JsonProperty("lang")]
    public string Lang { get; set; } = "en-us";

    public RPGGuild(ulong id)
    {
        this.Id = id;
    }
}