namespace NiTiS.RPGBot.Content.Items;

public interface IItemInstance
{
    [JsonIgnore]
    public Item Item { get; }
    [JsonProperty("id")]
    public string ItemID { get; }
    public uint Count { get; }
}