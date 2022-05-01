using System.Security.Cryptography;

namespace Transports.Plugins.Common;

public interface ICipherPlugin
{
    public string Name { get; }

    ICryptoTransform GetEncryptTransform();
    ICryptoTransform GetDecryptTransform();
}