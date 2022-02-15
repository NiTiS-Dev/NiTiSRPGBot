namespace NiTiS.RPGBot.Content;

public class Weapon : Item
{
    [JsonProperty("base_damage")]
    protected readonly int baseDamage;
    public Weapon(string id, int damage, int sellCost = -1) : base(id, sellCost)
    {
        this.baseDamage = damage;
    }

    [JsonProperty("type")]
    public WeaponType Type { get; set; }
    [JsonIgnore]
    public virtual int Damage => baseDamage;

    public override bool IsStackable => false;

}
public enum WeaponType : byte
{
    //Melle 1..20
    Sword = 1,
    Spear = 2,
    Hammer = 3,
    //Distance 21..40
    Bow = 21,
    Crossbow = 22,
    //Mage 41..60
    Book = 41,
    Wand = 42,

}