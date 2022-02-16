namespace NiTiS.RPGBot.Content.Items;

public class SingleItemInstance : IItemInstance
{
    [JsonIgnore]
    public Item Item { get; private set; }
    public string ItemID => Item.ID;
    [JsonIgnore]
    public uint Count => 1;
    [JsonConstructor]
    public SingleItemInstance(string itemID)
    {
        this.Item = Library.Search<Item>(itemID) ?? Item.Unknown(itemID);
    }
    public SingleItemInstance(Item item)
    {
        this.Item = item;
    }
}