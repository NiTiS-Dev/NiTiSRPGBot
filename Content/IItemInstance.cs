namespace NiTiS.RPGBot.Content;

public interface IItemInstance
{
    [JsonIgnore]
    public Item Item { get; }
    [JsonIgnore]
    public string ItemID { get; }
    [JsonIgnore]
    public uint Count { get; }
}