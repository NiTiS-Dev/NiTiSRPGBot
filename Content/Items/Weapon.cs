using Discord;
using NiTiS.Core.Additions;
using NiTiS.Core.Attributes;

namespace NiTiS.RPGBot.Content.Items;

public class Weapon : Item
{
    [JsonProperty("base_damage")]
    protected readonly int baseDamage;
    public Weapon(string id, int damage) : base(id)
    {
        this.baseDamage = damage;
    }

    [JsonProperty("type")]
    public WeaponType Type { get; set; }
    [JsonIgnore]
    public virtual int Damage => baseDamage;

    public override bool IsStackable => false;

    public override void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        base.AddFields(builder, rguild);
        string T_damage = rguild.GetTranslate("damage");
        string T_weaponType = rguild.GetTranslate("weapon-type");
        builder.AddField(T_damage, Damage);
        builder.AddField(T_weaponType, rguild.GetTranslate(Type.GetEnumValueName()));
    }

}
public enum WeaponType : byte
{
    //Melle 1..20
    [EnumInfo("weapon-type.sword")] Sword = 1,
    [EnumInfo("weapon-type.spear")] Spear = 2,
    [EnumInfo("weapon-type.hammer")] Hammer = 3,
    //Distance 21..40
    [EnumInfo("weapon-type.bow")] Bow = 21,
    [EnumInfo("weapon-type.crossbow")] Crossbow = 22,
    //Mage 41..60
    [EnumInfo("weapon-type.book")] Book = 41,
    [EnumInfo("weapon-type.wand")] Wand = 42,

}