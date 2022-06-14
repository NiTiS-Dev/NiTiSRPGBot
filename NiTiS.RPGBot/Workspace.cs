// The NiTiS-Dev licenses this file to you under the MIT license.

namespace NiTiS.RPGBot;
internal static class Workspace
{
	public const bool IsDebug =
#if DEBUG
		true;
#else
		false;
#endif
	public const ulong GuildID = 865340901521489950;
}
