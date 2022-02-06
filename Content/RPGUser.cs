using Newtonsoft.Json;

namespace NiTiS.RPGBot.Content;

public class RPGUser
{
    [JsonProperty("id")]
    public ulong Id { get; set; }
    [JsonProperty("admin")]
    public bool IsAdmin { get; set; }

    public RPGUser(ulong id)
    {
        this.Id = id;
    }
}
