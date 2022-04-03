using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using System;
using System.Drawing;

namespace NiTiS.Discord.RPGBot.Services;

public class BotLogService
{
	private readonly DiscordSocketClient client;
	public BotLogService(IServiceProvider provider)
	{
		this.client = provider.GetRequiredService<DiscordSocketClient>();

		client.Log += ClientLogAsync;
	}

	private Task ClientLogAsync(global::Discord.LogMessage log)
	{
		CS.WriteLine(log.ToString(), GetColor(log.Severity));
		return Task.CompletedTask;
	}
	public static Color GetColor(Severity severity)
	{
		return severity switch
		{
			Severity.Critical => Color.FromArgb(244, 80, 80),
			Severity.Error => Color.FromArgb(235, 172, 35),
			Severity.Verbose => Color.FromArgb(235, 120, 235),
			Severity.Debug => Color.FromArgb(120, 120, 120),
			Severity.Warning => Color.FromArgb(210, 120, 130),
			_ => Color.FromArgb(244, 244, 244)
		};
	}
}
