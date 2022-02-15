using NiTiS.Core.Attributes;

namespace NiTiS.RPGBot.Content;

public class Item : GameObject, ISellable
{
    [JsonProperty("rare")]
    public Rarity Rarity { get; set; } = Rarity.Common;
    [JsonProperty("base_sell_cost")]
    protected readonly int startCost = 0;
    [JsonIgnore]
    public virtual int SellCost => startCost;
    [JsonIgnore]
    public virtual bool IsStackable => true;

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
    public double GetRebootCoinModiferByRarity()
    {
        switch (Rarity)
        {
            case Rarity.Admin: return 0.0d;
            case Rarity.Uncommon: return 0.0001d;
            case Rarity.Rare: return 0.0005d;
            case Rarity.SuperRare: return 0.0065d;
            case Rarity.Epic: return 0.0735d;
            case Rarity.Legendary: return 0.75d;
            case Rarity.Godness: return 2.5d;
            default: return 0.0d;
        }
    }
}

public enum Rarity : byte
{
    [EnumInfo("rarity.none")] None = byte.MinValue,
    [EnumInfo("rarity.common")] Common = 1,
    [EnumInfo("rarity.uncommon")] Uncommon = 2,
    [EnumInfo("rarity.rare")] Rare = 3,
    [EnumInfo("rarity.super-rare")] SuperRare = 4,
    [EnumInfo("rarity.epic")] Epic = 5,
    [EnumInfo("rarity.legendary")] Legendary = 6,
    [EnumInfo("rarity.goodness")] Godness = 7,
    [EnumInfo("rarity.admin")] Admin = byte.MaxValue
}