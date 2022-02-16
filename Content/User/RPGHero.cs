using Discord;

namespace NiTiS.RPGBot.Content.User;

public class RPGHero : IEmbedContent
{
    public void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        XPModule.AddFields(builder, rguild);
        builder.AddField("Money", Money);
        builder.AddField("Items", Inventory.Count);
    }
    [JsonProperty("xp_module")]
    public XPModule XPModule { get; private set; } = new();
    [JsonProperty("gender")]
    public Gender Gender { get; internal set; } = Gender.NoGender;
    [JsonProperty("race")]
    public Race Race { get; internal set; } = Race.Human;
    [JsonProperty("inventory")]
    public Inventory Inventory { get; private set; } = new();
    [JsonProperty("money")]
    public uint Money { get; set; } = 500;
    [JsonConstructor()]
    public RPGHero()
    {

    }
    public uint CalculateRebootCoins()
    {
        uint amount = 0;
        //Every 1.000.000 coins give 1 rcoin
        amount += (uint)Math.Floor(Money / 1000000d);
        double rarityPoints = 0;
        foreach (StackableItemInstace item in Inventory)
        {
            rarityPoints += item.Item.RebootCoins * item.Count;
        }
        Console.WriteLine(rarityPoints);
        amount += (uint)Math.Round(rarityPoints);
        return amount;
    }
}