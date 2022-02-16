using System.Collections;

namespace NiTiS.RPGBot.Content;

public class Inventory : IEnumerable<IItemInstance>
{
    [JsonProperty("items")]
    private readonly List<IItemInstance> Items = new();
    [JsonIgnore]
    public int Count => Items.Count;

    public void AddItem(Item item, uint amount = 1)
    {
        if (item.IsStackable)
        {
            foreach(IItemInstance instance in Items)
            {
                if (instance.ItemID != item.ID) continue;
                if (instance is StackableItemInstace itemInstance)
                {
                    itemInstance.Count += amount;
                }
                return;
            }
            Items.Add(new StackableItemInstace(item, amount));
            return;
        }
        Items.AddRange(Enumerable.Repeat(new SingleItemInstance(item), (int)amount));
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
                Items.Remove(new StackableItemInstace(item));
            }
            return true;
        }
        Items.OfType<StackableItemInstace>().First((itm) => itm.ItemID == item.ID).Count -= requiredAmount;
        return true;
    }
    public uint GetCountOf(Item item)
    {
        uint count = 0;
        foreach(IItemInstance itemInstance in Items)
        {
            if (item.ID == itemInstance.ItemID)
            {
                count += itemInstance.Count;
            }
        }
        return count;
    }

    public IEnumerator<IItemInstance> GetEnumerator() => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
}