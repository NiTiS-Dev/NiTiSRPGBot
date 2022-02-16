using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.RPGBot.Content;

public static class Library
{
    private static readonly Dictionary<string, IRegistrable> library = new();
    public static void Registry(IRegistrable reg)
    {
        library[reg.ID] = reg;
        //If reg is ILocalizable to show localize key
        //Else just display id
        Console.WriteLine($"Library: <{reg.GetType()} {reg.ID}>" + ((reg is ILocalizable localizable) ? localizable.TranslateKey : "UNLOCALIZABLE") );
    }
    public static bool Unregistry(IRegistrable reg)
    {
        if (library.ContainsKey(reg.ID))
        {
            library.Remove(reg.ID);
            return true;
        }
        return false;
    }
    public static T? Search<T>(string id) where T : IRegistrable
    {
        if (library.ContainsKey(id))
        {
            return (T)library[id];
        }
        return default;
    }
    public static List<T> SearchAll<T>() where T : IRegistrable
    {
        return library.Values.OfType<T>().ToList();
    }
}