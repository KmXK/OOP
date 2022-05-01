using System.IO;
using System.Security.Cryptography;
using Transports.Plugins.Common;

namespace Transports.WPF;

public class PluginSerializer<T> : ISerializer<T>
{
    private const string SettingsFileName = "serializeSettings.json";

    private readonly ISerializer<T> _serializer;
    private ICipherPlugin _currentPlugin;

    public PluginSerializer(ISerializer<T> serializer)
    {
        _serializer = serializer;
    }

    public void Serialize(Stream stream, T data)
    {
        if (_currentPlugin != null)
        {
            using var cryptoStream = new CryptoStream(stream, _currentPlugin.GetEncryptTransform(), CryptoStreamMode.Write);
            _serializer.Serialize(cryptoStream, data);
        }
        else
        {
            _serializer.Serialize(stream, data);
        }

        SaveSettings();
    }

    public T Deserialize(Stream stream)
    {
        if (TryToLoadSettings(out var plugin))
        {
            using var cryptoStream = new CryptoStream(stream, plugin.GetDecryptTransform(), CryptoStreamMode.Read);
            return _serializer.Deserialize(cryptoStream);
        }

        return _serializer.Deserialize(stream);
    }

    public void SetPlugin(ICipherPlugin plugin)
    {
        _currentPlugin = plugin;
    }

    private void SaveSettings()
    {
        PluginSaver.Save(SettingsFileName, _currentPlugin);
    }

    private bool TryToLoadSettings(out ICipherPlugin plugin)
    {
        var pluginName = PluginSaver.TryLoad(SettingsFileName);
        if (pluginName != null)
        { 
            plugin = PluginLoader.Instance.GetPluginByName(pluginName);
            return plugin != null;
        }

        plugin = null;
        return false;
    }
}