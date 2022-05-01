using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Transports.Plugins.Common;

namespace Transports.WPF;

public class PluginLoader : IDisposable
{
    private const string PluginsFolder = "plugins";

    private static PluginLoader _instance;
    private static readonly object _instanceLock = new();

    private readonly List<ICipherPlugin> _plugins = new();

    private PluginLoader()
    {
        LoadPlugins();
    }

    public static PluginLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                {
                    _instance ??= new PluginLoader();
                }
            }

            return _instance;
        }
    }

    public IReadOnlyList<ICipherPlugin> Plugins => _plugins;

    public ICipherPlugin GetPluginByName(string name) => _plugins.FirstOrDefault(p => p.Name.Equals(name));

    private void LoadPlugins()
    {
        var pluginsFolder = Path.Combine(Environment.CurrentDirectory, PluginsFolder);

        if (Directory.Exists(pluginsFolder))
        {
            foreach (var filename in Directory.EnumerateFiles(pluginsFolder, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFile(filename);
                    LoadPluginsFromAssembly(assembly);
                }
                catch
                {
                    // Ignore
                }
            }
        }
    }

    private void LoadPluginsFromAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (typeof(ICipherPlugin).IsAssignableFrom(type))
            {
                if(Activator.CreateInstance(type) is ICipherPlugin plugin)
                    _plugins.Add(plugin);
            }
        }
    }

    public void Dispose()
    {
        foreach (var plugin in Plugins)
        {
            if(plugin is IDisposable p)
                p.Dispose();
        }
    }
}