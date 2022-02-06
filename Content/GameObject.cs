using Newtonsoft.Json;

namespace NiTiS.RPGBot.Content;

public class GameObject : IRegistrable, ILocalizable
{
    [JsonIgnore]
    private readonly string id;
    [JsonIgnore]
    private readonly string key;

    public GameObject(string id, string? key = null)
    {
        key ??= id;
        this.id = id;
        this.key = key;
    }
    [JsonProperty("id")]
    public string ID => id;
    [JsonProperty("key")]
    public string TranslateKey => key;
}