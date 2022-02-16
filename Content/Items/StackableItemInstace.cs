using NiTiS.RPGBot.Content.Registry;

namespace NiTiS.RPGBot.Content.Items;

public class StackableItemInstace : IItemInstance
{
    [JsonIgnore]
    public Item Item { get; private set; }
    public string ItemID => Item.ID;
    [JsonProperty("count")]
    public uint Count { get; set; }

    [JsonConstructor]
    public StackableItemInstace(string itemID, uint count = 1)
    {
        this.Item = Library.Search<Item>(itemID) ?? Item.Unknown(itemID);
        this.Count = count;
    }
    public StackableItemInstace(Item item, uint count = 1)
    {
        this.Item = item;
        this.Count = count;
    }
}