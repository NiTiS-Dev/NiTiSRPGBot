namespace NiTiS.RPGBot.Content;

public sealed class SingleItemInstance : IItemInstance
{
    [JsonIgnore]
    public Item Item { get; private set;}
    [JsonProperty("item_id")]
    public string ItemID => Item.ID;
    [JsonIgnore]
    public uint Count => 1;
    public SingleItemInstance(string item_id)
    {
        this.Item = Library.Search<Item>(item_id) ?? Item.Unknown(item_id);
    }
    public SingleItemInstance(Item item)
    {
        this.Item = item;
    }
}