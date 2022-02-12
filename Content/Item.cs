using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NiTiS.RPGBot.Content;

public class Item : GameObject, ISellable
{
    [JsonProperty("rare")]
    public Rarity Rarity { get; set; } = Rarity.Common;
    [JsonProperty("base_sell_cost")]
    protected readonly int startCost = 0;
    [JsonIgnore]
    public virtual int SellCost => startCost;

    public Item(string id, int startCost = -1) : base(id)
    {
        this.startCost = startCost;
    }
    private Item(object? nul, string id) : base(id, "lost." + id) { }
    public static Item Unknown(string id)
    {
        return new Item(null, id)
        {
            Rarity = 0,
        };
    }
}

public enum Rarity : byte
{
    None = 0,
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    SuperRare = 4,
    Epic = 5,
    Legendary = 6,
    Godness = 7,
    Admin = 255
}