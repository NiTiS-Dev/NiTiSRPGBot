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
    public void InitializeDirectory()
    {
        CheckFolder(DataDirectory);
        CheckFolder(WeaponsDirectory);
        CheckFolder(ItemsDirectory);
        CheckFolder(UsersDirectory);
        CheckFolder(GuildsDirectory);
        CheckFolder(TranslateDirectory);
    }
    public static void CheckFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
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
            Language? lang = JsonConvert.DeserializeObject<Language>(json);
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
            Item? item = JsonConvert.DeserializeObject<Item>(json);
            item.ID = Path.GetFileNameWithoutExtension(path);
            Library.Registry(item);
        }
        foreach (var path in Directory.GetFiles(WeaponsDirectory))
        {
            string json = File.ReadAllText(path);
            Weapon? weapon = JsonConvert.DeserializeObject<Weapon>(json);
            weapon.ID = Path.GetFileNameWithoutExtension(path);
            Library.Registry(weapon);
        }
    }
    #endregion
    #region Users
    private readonly Dictionary<ulong, RPGUser> cachedUsers = new();
    public RPGUser LoadUser(ulong id)
    {
        if(cachedUsers.ContainsKey(id)) 
            return cachedUsers[id];
        string path = PathToUser(id);
        RPGUser user;
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
    private readonly Dictionary<ulong, RPGGuild> cachedGuilds = new();
    public RPGGuild LoadGuild(ulong id)
    {
        if (cachedGuilds.ContainsKey(id))
            return cachedGuilds[id];
        string path = PathToGuild(id);
        RPGGuild guild;
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