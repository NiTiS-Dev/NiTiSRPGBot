namespace NiTiS.RPGBot.Content.Guild;

public class Language
{
    [JsonProperty("values")]
    private readonly Dictionary<string, string> values = new();
    [JsonProperty("code")]
    private readonly string code;
    [JsonProperty("name_en")]
    private readonly string englishName = "English";
    [JsonProperty("name")]
    private readonly string originalName = "English";

    public string OriginalName => originalName;
    public string EnglishName => englishName;
    public string Code => code;
    public Language()
    {
        this.code = "en-us";
    }
    public Language(string code)
    {
        this.code = code;
    }
    public string this[string key] => values[key];
    public bool Exists(string key)
    {
        return values.ContainsKey(key);
    }
    public string GetValue(string key)
    {
        return Language.GetTranslate(code, key);
    }
    public bool TryGetValue(string key, out string? value)
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
    private static readonly Dictionary<string, Language> languages = new();
    public static Language GetLanguage(string? code)
    {
        return languages[(code ?? "en-us")[0..5]];
    }
    public static void AddLanguage(Language language)
    {
        string code = language.code[0..5];
        languages[code] = language;
        Console.WriteLine($"Registry <{code}> {language.OriginalName} : {language.EnglishName}");
    }
    public static void AddLanguage(string code, Language language)
    {
        code = code[0..5];
        languages[code] = language;
    }
    public static bool LanguageExists(string code)
    {
        if (languages.ContainsKey(code))
            return true;

        foreach(Language lang in languages.Values)
        {
            if(lang.englishName.ToLower() == code) { return true; }
            if(lang.originalName.ToLower() == code) { return true; }
        }
        return false;
    }
    public static bool TryGetValue(string code, out Language? language)
    {
        foreach (Language lang in languages.Values)
        {
            if (lang.code.ToLower() == code) { language = lang; return true; }
            if (lang.englishName.ToLower() == code) { language = lang; return true; }
            if (lang.originalName.ToLower() == code) { language = lang; return true; }
        }
        language = null;
        return false;
    }
    public static List<Language> Languages => languages.Values.ToList();
    public static Dictionary<string, Language> Dictonary => languages;
    public static string GetTranslate(RPGGuild guild, string key) => GetTranslate(guild.Lang, key);
    public static string GetTranslate(string code, string key)
    {
        string? value;
        if (languages.TryGetValue(code, out var lang))
        {
            if(lang.TryGetValue(key, out value))
            {
                return value;
            }
        }
        if(languages["en-us"].TryGetValue(key, out value))
        {
            return value;
        }
        return $"<{code}>{key}";
    }
}
public static class LangCodes
{
    public const string English = "en-us";
    public const string Russian = "ru-ru";
}