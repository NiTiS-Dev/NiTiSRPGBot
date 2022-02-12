using Discord;

namespace NiTiS.RPGBot.Content;

public interface IEmbedContent
{
    public void AddFields(EmbedBuilder builder, RPGGuild rguild);
}