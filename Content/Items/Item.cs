using Discord;
using NiTiS.Core.Additions;

namespace NiTiS.RPGBot.Content.Items;

public class Item : GameObject, IEmbedContent
{
    [JsonProperty("rare")]
    public Rarity Rarity { get; protected set; } = Rarity.Common;
    [JsonIgnore]
    public virtual bool IsStackable => true;

    public Item(string id) : base(id) { }
    internal Item(object? _, string id) : base(id) { }
    public static Item Unknown(string id)
    {
        return new Item(null, id)
        {
            Rarity = 0,
        };
    }

    public virtual void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        string T_id = rguild.GetTranslate("id");
        string T_name = rguild.GetTranslate("item-name");
        string T_rarity = rguild.GetTranslate("rarity");
        string T_rebootCoins = rguild.GetTranslate("reboot-coins");
        builder.AddField(T_id, ID);
        builder.AddField(T_name, rguild.GetTranslate(ID + ".name"));
        builder.AddField(T_rarity, rguild.GetTranslate(Rarity.GetEnumValueName()));
        builder.AddField(T_rebootCoins, RebootCoins);
    }
    [JsonIgnore]
    public double RebootCoins
    {
        get
        {
            double func()
            {
                return Rarity switch
                {
                    Rarity.Admin => 0.0d,
                    Rarity.Uncommon => 0.0001d,
                    Rarity.Rare => 0.0005d,
                    Rarity.SuperRare => 0.0065d,
                    Rarity.Epic => 0.0735d,
                    Rarity.Legendary => 0.75d,
                    Rarity.Godness => 2.5d,
                    _ => 0.0d,
                };
            }
            return func() * (IsStackable ? 0.02d : 1);
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