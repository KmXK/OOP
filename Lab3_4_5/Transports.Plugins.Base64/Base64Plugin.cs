using System.Security.Cryptography;
using Transports.Plugins.Common;

namespace Transports.Plugins.Base64;

public class Base64Plugin : ICipherPlugin
{
    public string Name => "Base64";

    public ICryptoTransform GetEncryptTransform()
    {
        return new ToBase64Transform();
    }

    public ICryptoTransform GetDecryptTransform()
    {
        return new FromBase64Transform();
    }
}