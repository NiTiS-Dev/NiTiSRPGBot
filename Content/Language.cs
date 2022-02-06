namespace NiTiS.RPGBot.Content;

public class Language
{
    [JsonProperty("values")]
    private Dictionary<string, string> values = new();
    [JsonProperty("code")]
    private string code;
    public Language()
    {
        this.code = "en-us";
    }
    public Language(string code)
    {
        this.code = code;
    }
    public string this[string key] => values[key];

    public bool TryGetValue(string key, out string value)
    {
        if (this.values.ContainsKey(key))
        {
            value = this.values[key];
            return true;
        }
        value = null;
        return false;
    }

    //Static
    private static Dictionary<string, Language> languages = new();
    public static Language GetLanguage(string code)
    {
        return languages[code[0..5]];
    }
    public static void AddLanguage(Language language)
    {
        string code = language.code[0..5];
        languages[code] = language;
        Console.WriteLine($"Registry {code}");
    }
    public static void AddLanguage(string code, Language language)
    {
        code = code[0..5];
        languages[code] = language;
        Console.WriteLine($"Registry {code}");
    }
    public static string GetTranslate(RPGGuild guild, string key) => GetTranslate(guild.Lang, key);
    public static string GetTranslate(string code, string key)
    {
        if (languages.TryGetValue(code, out var lang))
        {
            if(lang.TryGetValue(key, out string value))
            {
                return value;
            }
        }
        return $"<{code}>{key}";
    }
}
public static class LangCodes
{
    public const string English = "en-US";
    public const string Russian = "ru-RU";
}