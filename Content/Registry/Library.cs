namespace NiTiS.RPGBot.Content.Registry;

public static class Library
{
    private static readonly Dictionary<string, IRegistrable<string>> library = new();
    public static void Registry(IRegistrable<string> reg)
    {
        library[reg.ID] = reg;
        //If reg is ILocalizable to show localize key
        //Else just display id
        Console.WriteLine($"Library: <{reg.GetType()} {reg.ID}>");
    }
    public static bool Unregistry(IRegistrable<string> reg)
    {
        if (library.ContainsKey(reg.ID))
        {
            library.Remove(reg.ID);
            return true;
        }
        return false;
    }
    public static T? Search<T>(string id) where T : IRegistrable<string>
    {
        if (library.ContainsKey(id))
        {
            return (T)library[id];
        }
        return default;
    }
    public static List<T> SearchAll<T>() where T : IRegistrable<string>
    {
        return library.Values.OfType<T>().ToList();
    }
}