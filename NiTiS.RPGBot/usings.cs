global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Threading;
global using System.Text;
global using Discord.Interactions;
global using Discord.WebSocket;
global using Discord;

global using SDir = System.IO.Directory;
global using Stream = System.IO.Stream;
global using SFile = System.IO.File;

public static class ServiceExtensions
{
	public static T GetRequiredService<T>(this IServiceProvider provider)
		=> (T)provider.GetService(typeof(T))!;
}