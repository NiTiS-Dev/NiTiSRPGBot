using System.Collections;

namespace NiTiS.RPGBot.Content;

public class Inventory : IEnumerable<ItemInstance>
{
    [JsonProperty("items")]
    private List<ItemInstance> Items = new();
    [JsonIgnore]
    public int Count => Items.Count;

    public void AddItem(Item item, uint amount = 1)
    {
        foreach(var itemInstance in Items)
        {
            if (itemInstance.ItemID != item.ID) continue;

            if (itemInstance.Item.IsStackable)
            {
                itemInstance.Count += amount;
                return;
            }
            else
            {
                Items.Add(new ItemInstance(item, 1 /*One bec-se item is not stackable*/));
                return;
            }
        }
    }
    /// <summary>
    /// Returns <see langword="true" /> when the required number (or more) of items are present
    /// </summary>
    public bool TryPopItem(Item item, uint requiredAmount = 1)
    {
        if (GetCountOf(item) < requiredAmount)
        {
            return false;
        }
        if(!item.IsStackable)
        {
            for(int i = 0; i < requiredAmount; i++)
            {
                Items.Remove(new ItemInstance(item));
            }
            return true;
        }
        Items.First((itm) => itm.ItemID == item.ID).Count -= requiredAmount;
        return true;
    }
    public uint GetCountOf(Item item)
    {
        uint count = 0;
        foreach(ItemInstance itemInstance in Items)
        {
            if (item.ID == itemInstance.ItemID)
            {
                count += itemInstance.Count;
            }
        }
        return count;
    }

    public IEnumerator<ItemInstance> GetEnumerator() => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
}