using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Content;

public class ItemInstance
{
    [JsonIgnore]
    public Item Item { get; private set; }
    [JsonProperty("item_id")]
    public string ItemID => Item.ID;
    [JsonProperty("count")]
    public int Count { get; set; }
    [JsonConstructor]
    public ItemInstance(string itemID)
    {
        this.Item = Library.Search<Item>(itemID) ?? Item.Unknown(itemID);
    }
    public ItemInstance(Item item)
    {
        this.Item = item;
    }
}