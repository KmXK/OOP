using System.IO;
using System.Text.Json;
using Transports.Plugins.Common;

namespace Transports.WPF;

public static class PluginSaver
{
    public static void Save(string filename, ICipherPlugin plugin)
    {
        File.Delete(filename);
        if (plugin != null)
        {
            File.WriteAllText(filename, JsonSerializer.Serialize(new PluginSettings(plugin.Name)));
        }
    }

    public static string TryLoad(string filename)
    {
        if (File.Exists(filename))
        {
            return JsonSerializer.Deserialize<PluginSettings>(File.ReadAllText(filename))?.Name;
        }

        return null;
    }

    private record PluginSettings(string Name);
}