// The NiTiS-Dev licenses this file to you under the MIT license.

namespace NiTiS.RPGBot.Module;
public class HelpModule : InteractionModuleBase<SocketInteractionContext>
{
	[SlashCommand("bitrate", "Gets the bitrate of a specific voice channel.")]
	public async Task GetBitrateAsync([ChannelTypes(ChannelType.Voice, ChannelType.Stage)] IChannel channel)
	{
		await RespondAsync(text: $"This voice channel has a bitrate of {(channel as IVoiceChannel)?.Bitrate ?? -1}");
	}
}
