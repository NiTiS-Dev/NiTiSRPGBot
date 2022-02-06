using Newtonsoft.Json;

namespace NiTiS.RPGBot.Content;

public class RPGUser
{
    [JsonProperty("id")]
    public ulong Id { get; set; } = ulong.MinValue;
    [JsonProperty("admin")]
    public bool IsAdmin { get; set; } = false;
    [JsonProperty("level")]
    public UInt16 Level { get; set; } = 1;
    [JsonProperty("xp")]
    public UInt32 XP { get; set; } = 0;

    public RPGUser(ulong id)
    {
        this.Id = id;
    }
}
