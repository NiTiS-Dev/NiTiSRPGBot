using NiTiS.RPGBot.Content;
using Newtonsoft.Json;

namespace NiTiS.RPGBot;

public class SaveModule
{
    private readonly string directory;
    public string DataDirectory => Path.Combine(directory, "Data");
    public string GuildsDirectory => Path.Combine(DataDirectory, "Guilds");
    public string ItemsDirectory => Path.Combine(DataDirectory, "Items");
    public string WeaponsDirectory => Path.Combine(ItemsDirectory, "Weapons");
    public string UsersDirectory => Path.Combine(DataDirectory, "Users");
    public string TranslateDirectory => Path.Combine(DataDirectory, "Translate");
    public SaveModule(string directory)
    {
        this.directory = directory;
    }
    public string PathToUser(ulong user) => Path.Combine(UsersDirectory, user.ToString() + ".json");
    public string PathToGuild(ulong guild) => Path.Combine(GuildsDirectory, guild.ToString() + ".json");
    public string PathToItem(string id) => Path.Combine(ItemsDirectory, id + ".json");
    public string PathToToken => Path.Combine(DataDirectory, "token.txt");
    #region Translate
    public void LoadLangs()
    {
        foreach (var path in Directory.GetFiles(TranslateDirectory))
        {
            string json = File.ReadAllText(path);
            Language lang = JsonConvert.DeserializeObject<Language>(json);
            Language.AddLanguage(lang);
        }
    }
    #endregion
    #region Items
    public void LoadItems()
    {
        foreach(var path in Directory.GetFiles(ItemsDirectory))
        {
            string json = File.ReadAllText(path);
            JsonConvert.DeserializeObject<Item>(json)?.Registry();
        }
        foreach (var path in Directory.GetFiles(WeaponsDirectory))
        {
            string json = File.ReadAllText(path);
            JsonConvert.DeserializeObject<Weapon>(json)?.Registry();
        }
    }
    #endregion
    #region Users
    private Dictionary<ulong, RPGUser> cachedUsers = new();
    public RPGUser LoadUser(ulong id)
    {
        if(cachedUsers.ContainsKey(id)) 
            return cachedUsers[id];
        string path = PathToUser(id);
        RPGUser user = null;
        if (!File.Exists(path))
        {
            user = new RPGUser(id);
            Write(path, user);
            
        }
        else
        {
            user = Read<RPGUser>(path);
        }
        cachedUsers[id] = user;
        return user;
    }
    public void SaveUsers()
    {
        foreach(var user in cachedUsers.Values)
        {
            Write(PathToUser(user.Id), user);
        }
    }
    public void ClearPlayersCache() => cachedUsers.Clear();
    #endregion
    #region Guilds
    private Dictionary<ulong, RPGGuild> cachedGuilds = new();
    public RPGGuild LoadGuild(ulong id)
    {
        if (cachedUsers.ContainsKey(id))
            return cachedGuilds[id];
        string path = PathToUser(id);
        RPGGuild guild = null;
        if (!File.Exists(path))
        {
            guild = new RPGGuild(id);
            Write(path, guild);

        }
        else
        {
            guild = Read<RPGGuild>(path);
        }
        return cachedGuilds[id] = guild;
    }
    public void SaveGuilds()
    {
        foreach (var guild in cachedGuilds.Values)
        {
            Write(PathToGuild(guild.Id), guild);
        }
    }
    public void ClearGuildsCache() => cachedGuilds.Clear();
    #endregion
    public static void Write(string path, object write, Formatting formatting = Formatting.Indented)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(write, formatting));
    }
    public static T? Read<T>(string path)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(path)) ?? default;
    }
    public string LoadToken() => File.ReadAllText(PathToToken);
}