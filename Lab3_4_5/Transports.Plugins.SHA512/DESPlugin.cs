using System.Security.Cryptography;
using Transports.Plugins.Common;

namespace Transports.Plugins.DES;

public class DESPlugin : ICipherPlugin
{
    public string Name => "DES";

    public ICryptoTransform GetEncryptTransform()
    {
        using var des = System.Security.Cryptography.DES.Create();
        return des.CreateEncryptor();
    }

    public ICryptoTransform GetDecryptTransform()
    {
        using var des = System.Security.Cryptography.DES.Create();
        return des.CreateDecryptor();
    }
}