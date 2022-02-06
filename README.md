# NiTiSRPGBot

```cs
using Discord;
using NiTiS.Core.Collections;
using NiTiS.RPGBot;
...
bot = new RPGBot("OTM4OTEwMTEyMDgzNDc2NTIy.YfxKLg.ud4XH2zBMzA_DueIM5QXgRBV5Hc");
bot.Startup();
while (true)
{
  Task.Delay(1000 * 60 * 2).Wait();
  SaveModule saveModule = SingletonManager.GetInstance<SaveModule>();
  saveModule.SaveUsers();
  Console.WriteLine("Saved!");
}
```
