using Discord;
using NiTiS.Core.Math;

namespace NiTiS.RPGBot.Content.User;

public class XPModule : IEmbedContent
{
    [JsonIgnore]
    private GeometricProgression progression = new(5, 1.25);
    [JsonProperty("level")]
    public UInt16 Level { get; private set; } = 1;
    [JsonProperty("xp")]
    public UInt64 XP { get; private set; } = 0;
    [JsonIgnore]
    public UInt64 RequiredXP => (uint)Math.Round(progression.Get(Level));
    [JsonIgnore]
    public bool IsMaximumLevel => Level >= 150;

    public void AddFields(EmbedBuilder builder, RPGGuild rguild)
    {
        builder.AddField("Level", Level + (IsMaximumLevel ? " (Max Level)" : ""));
        builder.AddField("XP", IsMaximumLevel ? "INFINITY" : $"{XP} / {RequiredXP}");
    }

    public void ApplyXP(UInt64 xp)
    {
        if (IsMaximumLevel)
        {

        }
        if (xp <= 0)
        {
            return;
        }
        ulong got = XP + xp;
        if (RequiredXP <= got)
        {
            ulong rest = got - RequiredXP;
            Level++;
            XP = 0;
            ApplyXP(rest);
        }
        else
        {
            XP += xp;
        }
    }
}