using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NiTiS.RPGBot.Content;

public class Item : GameObject
{
    [JsonProperty("rare")]
    public Rarity Rarity { get; set; } = Rarity.Common;
    [JsonIgnore]
    public virtual int SellCost { get; }
    public Item(string id) : base(id)
    {

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