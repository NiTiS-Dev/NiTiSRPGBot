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
    public uint Count { get; set; }
    [JsonConstructor]
    public ItemInstance(string item_id /*Named like in json*/)
    {
        this.Item = Library.Search<Item>(item_id) ?? Item.Unknown(item_id);
    }
    public ItemInstance(Item item, uint count = 1)
    {
        this.Item = item;
        this.Count = count;
    }
}