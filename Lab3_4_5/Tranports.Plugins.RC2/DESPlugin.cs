using System.Security.Cryptography;
using System.Text;
using Transports.Plugins.Common;

namespace Transports.Plugins.DES;

public class DESPlugin : ICipherPlugin, IDisposable
{
    private readonly System.Security.Cryptography.DES _des;

    public DESPlugin()
    {
        _des = System.Security.Cryptography.DES.Create();
        _des.Key = Encoding.ASCII.GetBytes("ABCDEFGH");
        _des.IV = Encoding.ASCII.GetBytes("ABCDEFGH");
    }

    public string Name => "DES";

    public ICryptoTransform GetEncryptTransform()
    {
        return _des.CreateEncryptor();
    }

    public ICryptoTransform GetDecryptTransform()
    {
        return _des.CreateDecryptor();
    }

    public void Dispose()
    {
        _des.Dispose();
    }
}