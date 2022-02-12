using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Content;

public class Inventory
{
    [JsonProperty("items")]
    private List<ItemInstance> Items = new();
    [JsonIgnore]
    public int Count => Items.Count;

    public void AddItem(Item item)
    {
        foreach(var itemInstance in Items)
        {
            if (itemInstance.ItemID == item.ID)
            {
                itemInstance.Count++;
                return;
            }
        }
    }
}