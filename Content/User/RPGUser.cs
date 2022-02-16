using Discord;
using NiTiS.Core.Additions;

namespace NiTiS.RPGBot.Content.User;

public class RPGUser : IEmbedContent
{
    [JsonProperty("id")]
    public ulong Id { get; set; } = ulong.MinValue;
    [JsonProperty("admin")]
    public bool IsAdmin { get; set; } = false;
    [JsonProperty("reset_shards")]
    public uint ResetShards { get; set; } = 0;
    [JsonProperty("hero")]
    public RPGHero? Hero { get; set; }
    [JsonConstructor()]
    public RPGUser()
    {

    }
    public RPGUser(ulong id)
    {
        this.Id = id;
    }

    public void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        string T_id = rguild.GetTranslate("id");
        string T_admin = rguild.GetTranslate("admin");
        string T_gender = rguild.GetTranslate("gender");
        string T_race = rguild.GetTranslate("race");

        builder.AddField(T_id, Id);
        builder.AddField(T_admin, rguild.GetTranslate(IsAdmin ? "bool.true" : "bool.false"));
        if(Hero is not null)
        {
            builder.AddField(T_race, rguild.GetTranslate(Hero.Race.GetEnumValueName()));
            builder.AddField(T_gender, rguild.GetTranslate(Hero.Gender.GetEnumValueName()));
        }
    }
}
