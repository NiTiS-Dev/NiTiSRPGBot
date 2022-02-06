# NiTiSRPGBot

```cs
using Discord;
using NiTiS.Core.Collections;
using NiTiS.RPGBot;
...
bot = new RPGBot("token");
bot.Startup();
while (true)
{
  Task.Delay(1000 * 60 * 2).Wait();
  SaveModule saveModule = SingletonManager.GetInstance<SaveModule>();
  saveModule.SaveUsers();
  Console.WriteLine("Saved!");
}
```
