namespace NiTiS.RPGBot.Modules.Utils;

public static class CommandHelper
{
    public static string GetCommandNameKey(string commandName) => "cmd." + commandName;
    public static string GetCommandDescriptionKey(string commandName) => "cmd." + commandName + ".description";
    public static string GetCommandUsageKey(string commandName) => "cmd." + commandName + ".usage";
    public static string GetCommandTabNameKey(string tabName) => "cmd-tab." + tabName;
    public static string GetCommandTabDescriptionKey(string tabName) => "cmd-tab." + tabName + ".description";
}