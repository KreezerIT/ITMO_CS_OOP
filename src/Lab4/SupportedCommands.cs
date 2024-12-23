using System.ComponentModel;
using System.Reflection;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public static class SupportedCommands
{
    public static bool IsCommandSupported(string command)
    {
        return typeof(SupportedListCommands)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Any(field => field.GetCustomAttribute<DescriptionAttribute>()?.Description == command);
    }

    public enum SupportedListCommands
    {
        [Description("connect")]
        Connect,

        [Description("disconnect")]
        Disconnect,

        [Description("tree goto")]
        TreeToGo,

        [Description($"tree list")]
        TreeList,

        [Description("file show")]
        FileShow,

        [Description("file move")]
        FileMove,

        [Description("file copy")]
        FileCopy,

        [Description("file delete")]
        FileDelete,

        [Description("file rename")]
        FileRename,
    }
}