<p align="center">
  <img src="https://github.com/NickName73/NickName73/blob/main/Micros/nitis-rpg-logo.png?raw=true">
  <h1 align="center">
    Discord RPG Bot on NiTiS.Core and Discord.Net
  </h1>
</p>

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
  saveModule.SaveGuilds();
  Console.WriteLine("Saved!");
}
```
