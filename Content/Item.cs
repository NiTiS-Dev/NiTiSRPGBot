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
}

public enum Rarity : byte
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    SuperRare = 3,
    Epic = 4,
    Legendary = 5,
    Godness = 6,
    Admin = 255
}