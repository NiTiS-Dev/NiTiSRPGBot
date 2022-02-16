namespace NiTiS.RPGBot.Content;

public class StackableItemInstace : IItemInstance
{
    [JsonIgnore]
    public Item Item { get; private set; }
    [JsonProperty("item_id")]
    public string ItemID => Item.ID;
    [JsonProperty("count")]
    public uint Count { get; set; }
    [JsonConstructor]
    public StackableItemInstace(string item_id, uint count = 1)
    {
        this.Item = Library.Search<Item>(item_id) ?? Item.Unknown(item_id);
        this.Count = count;
    }
    public StackableItemInstace(Item item, uint count = 1)
    {
        this.Item = item;
        this.Count = count;
    }
}